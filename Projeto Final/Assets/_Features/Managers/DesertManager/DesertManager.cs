using UnityEngine;
using System.Collections.Generic;

public class DesertManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject desertTilePrefab; 
    public int tileSize; 

    public Dictionary<Vector2Int, DesertTile> tiles = new Dictionary<Vector2Int, DesertTile>(); // Armazena os DesertTiles j� criados

    void Start()
    {
        if (player == null) player = GameObject.FindWithTag("Player");
        tileSize = desertTilePrefab.GetComponent<DesertTile>().tileSize;
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

    public Vector2Int GetTileCoord(Vector3 position)
    {
        int x = Mathf.FloorToInt(position.x / tileSize * .1f);
        int z = Mathf.FloorToInt(position.z / tileSize * .1f);
        return new Vector2Int(x, z);
    }

    private void SpawnTile(Vector2Int coord)
    {
        if (tiles.ContainsKey(coord)) return;

        Vector3 position = new Vector3(coord.x * tileSize * .1f, transform.position.y - 10f, coord.y * tileSize * .1f);


        GameObject newTile = Instantiate(desertTilePrefab, position, Quaternion.identity, transform);

        DesertTile tileComponent = newTile.GetComponent<DesertTile>();
        tileComponent.Initialize(coord);

        tiles.Add(coord, tileComponent);
    }
}
