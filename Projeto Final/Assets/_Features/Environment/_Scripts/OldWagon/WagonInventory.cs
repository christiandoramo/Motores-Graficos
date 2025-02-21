using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WagonInventory : MonoBehaviour
{
    public Dictionary<ItemType, Item> inventory = new Dictionary<ItemType, Item>(); // Armazena os TerrainTiles já criados


    [SerializeField] private Vector3 boxSize;
    [SerializeField]
    private LayerMask carryableMask;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }

    // criar classe separada para NPCs, Impostores fazer Gather
    public void SaveItem()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, boxSize * .5f, Quaternion.identity, carryableMask);
        Collider collider = colliders.FirstOrDefault((collider) => collider != null);

        if (colliders.Length == 0 || collider == null)
        {
            GameObject carryable = collider.gameObject;
            carryable.GetComponent<Carryable>().SaveItemAmount(inventory);
        }
    }

    //public void UseItem(InputAction.CallbackContext ctx)
    //{

    //    // Item.ExecuteEffect(WagonInventory,ctx.itemType)
    //}
}
