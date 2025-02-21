using UnityEngine;

public class TerrainTile : MonoBehaviour
{
    [SerializeField] private int resolution = 10; // Resolução da malha
    [SerializeField] private float maxHeight = 100f; // Altura máxima das dunas
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
        GenerateTerrain(tileCoord);
    }

    private void GenerateTerrain(Vector2Int tileCoord)
    {
        // Cria um objeto TerrainData
        TerrainData terrainData = new TerrainData();

        // Define a resolução do terreno
        terrainData.heightmapResolution = resolution + 1;

        // Cria a matriz de alturas
        float[,] heights = new float[resolution + 1, resolution + 1];

        // Offsets baseados no tileCoord para garantir continuidade entre tiles
        float xOffset = tileCoord.x * resolution * noiseScale;
        float zOffset = tileCoord.y * resolution * noiseScale;

        // Gera deslocamentos aleatórios para o Perlin Noise
        float randomOffsetX = Random.Range(0f, 1000f);
        float randomOffsetZ = Random.Range(0f, 1000f);

        for (int z = 0; z <= resolution; z++)
        {
            for (int x = 0; x <= resolution; x++)
            {
                // Perlin Noise multi-octave com deslocamento aleatório
                float height = GeneratePerlinNoise(
                    x * noiseScale + xOffset + randomOffsetX,
                    z * noiseScale + zOffset + randomOffsetZ
                );

                heights[z, x] = height; // Altura normalizada para o TerrainData
            }
        }

        // Aplica as alturas ao TerrainData
        terrainData.size = new Vector3(tileSize, maxHeight, tileSize); // Define tamanho do terreno
        terrainData.SetHeights(0, 0, heights);

        // Cria um GameObject com Terrain
        GameObject terrainObj = new GameObject($"Tile_{tileCoord.x}_{tileCoord.y}");
        terrainObj.transform.position = new Vector3(tileCoord.x * tileSize, 0, tileCoord.y * tileSize);

        Terrain terrain = terrainObj.AddComponent<Terrain>();
        terrain.terrainData = terrainData;

        // Adiciona o TerrainCollider
        TerrainCollider terrainCollider = terrainObj.AddComponent<TerrainCollider>();
        terrainCollider.terrainData = terrainData;
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

        return (total / maxValue) * maxHeight; // Normaliza o valor para a altura máxima
    }

    public void RegenerateMesh()
    {
        if (meshCollider == null || meshFilter == null) return;
        // Gera o mesh usando uma coordenada padrão (exemplo: Vector2Int.zero)
        GenerateTerrain(Vector2Int.zero);
    }
}
