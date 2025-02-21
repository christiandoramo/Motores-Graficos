using UnityEngine;
using System.Collections.Generic;

public class TerrainManager : MonoBehaviour
{
    [SerializeField] private GameObject player; // Referência ao player
    [SerializeField] private GameObject TerrainTilePrefab; // Prefab do TerrainTile
    private int tileSize; // Tamanho de cada tile

    private Dictionary<Vector2Int, TerrainTile> tiles = new Dictionary<Vector2Int, TerrainTile>(); // Armazena os TerrainTiles já criados

    void Start()
    {
        if (player == null) player = GameObject.FindWithTag("Player");
        tileSize = TerrainTilePrefab.GetComponent<TerrainTile>().tileSize;
        // Cria o primeiro tile onde o player está
        Vector2Int playerTileCoord = GetTileCoord(player.transform.position);
        SpawnTile(playerTileCoord);
    }

    void Update()
    {
        Vector2Int playerTileCoord = GetTileCoord(player.transform.position);

        // Garante que o tile do player e os adjacentes estejam carregados
        for (int x = -1; x <= 1; x++)
        {
            for (int z = -1; z <= 1; z++)
            {
                Vector2Int coord = playerTileCoord + new Vector2Int(x, z);
                if (!tiles.ContainsKey(coord))
                {
                    SpawnTile(coord);
                }
            }
        }
    }

    private Vector2Int GetTileCoord(Vector3 position)
    {
        int x = Mathf.FloorToInt(position.x / tileSize);
        int z = Mathf.FloorToInt(position.z / tileSize);
        return new Vector2Int(x, z);
    }

    private void SpawnTile(Vector2Int coord)
    {
        if (tiles.ContainsKey(coord)) return;

        Vector3 position = new Vector3(coord.x * tileSize, transform.position.y - 10f, coord.y * tileSize);


        GameObject newTile = Instantiate(TerrainTilePrefab, position, Quaternion.identity, transform);

        TerrainTile tileComponent = newTile.GetComponent<TerrainTile>();
        tileComponent.Initialize(coord); // Inicializa o TerrainTile com informações específicas, se necessário

        tiles.Add(coord, tileComponent);
    }
}
