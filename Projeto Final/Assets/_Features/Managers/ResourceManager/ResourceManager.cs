using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] LayerMask gatherableMask;
    [SerializeField] Vector3 caldeironBoxSize, pavillionBoxSize;
    [SerializeField] Transform caldeironTransform, pavillionTransform;
    private Dictionary<int, Gatherable> itemsInCalderon = new Dictionary<int, Gatherable>();
    private Dictionary<int, Gatherable> gatherablesInPavillion = new Dictionary<int, Gatherable>();

    public int wood, oil, water, totalWood, totalOil, totalWater, load;
    private const int maxLoad = 5;
    public bool AutoDriveActivated;

    void LateUpdate()
    {
        CheckItems();
        ProcessResources();
        UpdateHUD();
    }

    private void CheckItems()
    {
        CheckItemsInArea(pavillionTransform.position, pavillionBoxSize, gatherablesInPavillion);
        CheckItemsInArea(caldeironTransform.position, caldeironBoxSize, itemsInCalderon);
    }

    private void CheckItemsInArea(Vector3 position, Vector3 boxSize, Dictionary<int, Gatherable> items)
    {
        Collider[] colliders = Physics.OverlapBox(position, boxSize * .5f, Quaternion.identity, gatherableMask);
        HashSet<int> currentIds = new HashSet<int>();

        foreach (var collider in colliders)
        {
            if (collider != null)
            {
                GameObject gatherable = collider.gameObject;
                AddItem(gatherable, items);
                currentIds.Add(gatherable.GetInstanceID());
            }
        }
        RemoveMissingItems(currentIds, items);
    }

    void AddItem(GameObject obj, Dictionary<int, Gatherable> items)
    {
        if (!obj.TryGetComponent<Gatherable>(out var ga)) return;
        int id = obj.GetInstanceID();

        if (!items.ContainsKey(id))
        {
            items[id] = ga;
        }
    }

    void RemoveMissingItems(HashSet<int> currentIds, Dictionary<int, Gatherable> items)
    {
        var keysToRemove = items.Keys.Where(id => !currentIds.Contains(id)).ToList();
        foreach (var id in keysToRemove)
        {
            items.Remove(id);
        }
    }

    private void ProcessResources()
    {
        totalWood = gatherablesInPavillion.Values.Count(g => g.collectibleType == Collectible.Wood);
        totalOil = gatherablesInPavillion.Values.Count(g => g.collectibleType == Collectible.Oil);
        totalWater = gatherablesInPavillion.Values.Count(g => g.collectibleType == Collectible.Water);
        wood = itemsInCalderon.Values.Count(g => g.collectibleType == Collectible.Wood);
        oil = itemsInCalderon.Values.Count(g => g.collectibleType == Collectible.Oil);
        water = itemsInCalderon.Values.Count(g => g.collectibleType == Collectible.Water);

        if (wood > 0 && oil > 0)
        {
            RemoveItem(Collectible.Wood, itemsInCalderon, gatherablesInPavillion);
            RemoveItem(Collectible.Oil, itemsInCalderon, gatherablesInPavillion);
            load++;
        }

        if (water > 0)
        {
            RemoveItem(Collectible.Water, itemsInCalderon, gatherablesInPavillion);
            load = 0;
        }

        AutoDriveActivated = load >= maxLoad;
    }

    private void RemoveItem(Collectible type, Dictionary<int, Gatherable> calderon, Dictionary<int, Gatherable> pavillion)
    {
        var itemToRemove = calderon.FirstOrDefault(kv => kv.Value.collectibleType == type);
        if (!itemToRemove.Equals(default(KeyValuePair<int, Gatherable>)))
        {
            Destroy(itemToRemove.Value.gameObject);
            calderon.Remove(itemToRemove.Key);
            pavillion.Remove(itemToRemove.Key);
        }
    }

    private void UpdateHUD()
    {
        GameManager.instance.hudManager.UpdateOilCount(oil, totalOil);
        GameManager.instance.hudManager.UpdateWaterCount(water, totalWater);

        GameManager.instance.hudManager.UpdateWoodCount(wood, totalWood);

        GameManager.instance.hudManager.UpdateLoadCount(load, maxLoad);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(caldeironTransform.position, caldeironBoxSize);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(pavillionTransform.position, pavillionBoxSize);
    }
}
