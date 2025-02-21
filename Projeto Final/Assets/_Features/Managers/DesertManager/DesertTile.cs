using UnityEngine;

public class DesertTile : MonoBehaviour
{
    [SerializeField] private int resolution = 10; // Resolução da malha
    [SerializeField] private float duneHeight = 100f; // Altura máxima das dunas
    [SerializeField] private float noiseScale = 0.1f; // Escala do Perlin Noise
    public int tileSize = 10; // Tamanho físico do tile
    [SerializeField] private int octaves = 3; // Número de camadas de Perlin Noise
    [SerializeField] private float persistence = 0.5f; // Influência de cada camada de Noise
    [SerializeField] private float lacunarity = 2.0f; // Detalhamento das camadas
    [SerializeField] private int seed = 42; // Semente para gerar aleatoriedade


    private MeshFilter meshFilter;
    private MeshCollider meshCollider;


    public void Initialize(Vector2Int tileCoord)
    {
        // Configurações iniciais do tile
        name = $"DesertTile {tileCoord.x}, {tileCoord.y}";
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
        GenerateMesh(tileCoord);
    }

    private void GenerateMesh(Vector2Int tileCoord)
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[(resolution + 1) * (resolution + 1)];
        int[] triangles = new int[resolution * resolution * 6];

        // Offsets baseados no tileCoord para garantir continuidade entre tiles
        float xOffset = tileCoord.x * resolution * noiseScale;
        float zOffset = tileCoord.y * resolution * noiseScale;

        // Gera deslocamentos aleatórios para o Perlin Noise
        float randomOffsetX = Random.Range(0f, 1000f);
        float randomOffsetZ = Random.Range(0f, 1000f);

        // Fatores de suavização para cada borda
        float topSmoothness = Random.Range(1f, 2f);
        float bottomSmoothness = Random.Range(1f, 2f);
        float leftSmoothness = Random.Range(1f, 2f);
        float rightSmoothness = Random.Range(1f, 2f);

        // Gera vértices com Perlin Noise
        for (int z = 0, i = 0; z <= resolution; z++)
        {
            for (int x = 0; x <= resolution; x++, i++)
            {
                // Perlin Noise multi-octave com deslocamento aleatório
                float height = GeneratePerlinNoise(
                    x * noiseScale + xOffset + randomOffsetX,
                    z * noiseScale + zOffset + randomOffsetZ
                );

                // Suavização nas bordas com fatores individuais
                float topFactor = Mathf.Lerp(1f, 0f, z / (float)resolution) * topSmoothness;
                float bottomFactor = Mathf.Lerp(1f, 0f, 1f - (z / (float)resolution)) * bottomSmoothness;
                float leftFactor = Mathf.Lerp(1f, 0f, x / (float)resolution) * leftSmoothness;
                float rightFactor = Mathf.Lerp(1f, 0f, 1f - (x / (float)resolution)) * rightSmoothness;

                // Combina os fatores para calcular o edgeFactor
                float edgeFactor = Mathf.Min(topFactor, bottomFactor, leftFactor, rightFactor);
                edgeFactor = Mathf.Clamp01(edgeFactor); // Garante que esteja no intervalo [0, 1]

                height *= Mathf.Pow(edgeFactor, 2); // Aplica suavização

                vertices[i] = new Vector3(x * tileSize / resolution, height, z * tileSize / resolution);
            }
        }

        // Gera os triângulos da malha
        for (int z = 0, ti = 0, vi = 0; z < resolution; z++, vi++)
        {
            for (int x = 0; x < resolution; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 1] = vi + resolution + 1;
                triangles[ti + 2] = vi + 1;
                triangles[ti + 3] = vi + 1;
                triangles[ti + 4] = vi + resolution + 1;
                triangles[ti + 5] = vi + resolution + 2;
            }
        }

        // Configura a malha
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;

        // Atualiza o MeshCollider
        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = mesh;
    }

    private float GeneratePerlinNoise(float x, float z)
    {
        float total = 0;
        float amplitude = 1;
        float frequency = 1;
        float maxValue = 0;

        for (int i = 0; i < octaves; i++)
        {
            total += Mathf.PerlinNoise((x + seed) * frequency, (z + seed) * frequency) * amplitude;
            maxValue += amplitude;

            amplitude *= persistence;
            frequency *= lacunarity;
        }

        return (total / maxValue) * duneHeight; // Normaliza o valor para a altura máxima
    }

    public void RegenerateMesh()
    {
        if (meshCollider == null || meshFilter == null) return;
        // Gera o mesh usando uma coordenada padrão (exemplo: Vector2Int.zero)
        GenerateMesh(Vector2Int.zero);
    }
}
