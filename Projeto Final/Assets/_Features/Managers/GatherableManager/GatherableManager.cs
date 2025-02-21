using System.Collections.Generic;
using UnityEngine;

// COLOCAR ITENS SPAWNAVEIS USANDO ESSE ARQUIVO E COLOCAR NO DESERTMANAGER
public class GatherableManager : MonoBehaviour
{
    [Header("Colet�veis (Gatherables)")]
    [Tooltip("Armazena os tile j� criados")]
    [SerializeField] private Dictionary<Vector2Int, bool> tilesWithGatherableSpawned = new Dictionary<Vector2Int, bool>();

    [Tooltip("Armazena os gatherables j� criados")]
    [SerializeField] private List<GameObject> gatherablesSpawned = new List<GameObject>();

    [Tooltip("Dist�ncia m�nima entre gatherables (serve para dist�ncia x e z)")]
    [SerializeField] private float gatherableDistance = 2f;

    [Tooltip("Quantidade de gatherables por DesertTile")]
    [SerializeField] private int gatherableToSpawnByDesertTile = 5;

    [Tooltip("Prefabs de gatherables (com Script Gatherable)")]
    [SerializeField] private List<GameObject> gatherablePrefabs;

    private DesertManager dm;

    void Start()
    {
        if (dm == null) dm = GameObject.Find("Managers/DesertManager").GetComponent<DesertManager>();
    }

    public void SpawnGatherablesByDesertTileEnabled(Vector2Int desertTileCoord, DesertTile desertTile)
    {
        if (tilesWithGatherableSpawned.ContainsKey(desertTileCoord) && tilesWithGatherableSpawned[desertTileCoord])
        {
            return; 
        }


        tilesWithGatherableSpawned[desertTileCoord] = true;

        for (int i = 0; i < gatherableToSpawnByDesertTile; i++)
        {
            SpawnGatherable(desertTileCoord, desertTile);
        }
    }

    private void SpawnGatherable(Vector2Int desertTileCoord, DesertTile desertTile)
    {
        Vector3 tilePosition = desertTile.transform.position;

        float spawnRange = desertTile.tileSize / 2f - gatherableDistance;

        Vector3 randomPosition;
        bool validPosition = false;

        int maxAttempts = 100;
        int attempts = 0;

        do
        {
            float randomX = Random.Range(-spawnRange, spawnRange);
            float randomZ = Random.Range(-spawnRange, spawnRange);
            randomPosition = new Vector3(tilePosition.x + randomX, tilePosition.y, tilePosition.z + randomZ);

            validPosition = true;
            foreach (GameObject spawned in gatherablesSpawned)
            {
                if (Vector3.Distance(randomPosition, spawned.transform.position) < gatherableDistance)
                {
                    validPosition = false;
                    break;
                }
            }

            attempts++;
        } while (!validPosition && attempts < maxAttempts);

        if (validPosition)
        {
            // Escolhe um prefab aleat�rio
            GameObject gatherablePrefab = gatherablePrefabs[Random.Range(0, gatherablePrefabs.Count)];

            // Instancia o gatherable
            GameObject gatherable = Instantiate(gatherablePrefab, randomPosition, Quaternion.identity);

            // Adiciona � lista de gatherables spawnados
            gatherablesSpawned.Add(gatherable);
        }
        else
        {
            Debug.LogWarning($"N�o foi poss�vel encontrar uma posi��o v�lida para spawnar um gatherable no tile {desertTileCoord} ap�s {attempts} tentativas.");
        }
    }
}
