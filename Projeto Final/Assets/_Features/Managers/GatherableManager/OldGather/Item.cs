using UnityEngine;
using UnityEngine.UI;

// ver tutorial
public class Item
{
    public int amount;
    public ItemType itemType; // tipo direto de item
    //public WeaponableType itemType; // tipo de arma - empunh�vel
    //public ConsumableType itemType; // tipo de consum�vel
    //public BuildingType itemType; // tipo de constru��o
    public UtilityType utilityType; // tipo generico de item - empunh�vel, consumivel, constru��o
    public Image image;

    //public void ExecuteEffect(WagonInventory wi)
    //{
    //    Dictionary<ItemType, Item> inventory = wi.inventory;
    //    switch (UtilityType)
    //    {

    //        case UtilityType.consumable:
    //            {
    //                Consume(inventory);
    //            }
    //            break;
    //        case UtilityType.craftable:
    //            {
    //                BuildingCraft(inventory); // craft constroi uma constru��o
    //            }
    //            break;
    //        case UtilityType.craftable:
    //            {
    //                Craft(inventory);
    //            }
    //            break;
    //        default:
    //            break;
    //    }
    //}

    //public void Consume(inventory) // apenas �gua e fcomida
    //{
    //    switch (itemType)
    //    {
    //        case ItemType.water:
    //            {

    //            }
    //            break;
    //        case ItemType.food:
    //            {

    //            }
    //            break;
    //        default:
    //            break;
    //    }
    //}
    public void Craft() // apenas �gua e fcomida
    {
        //    switch (itemType)
        //    {
        //        case ItemType.stone:
        //            {

        //            }
        //            break;
        //        case ItemType.water:
        //            {

        //            }
        //            break;
        //        case ItemType.food:
        //            {

        //            }
        //            break;
        //        case ItemType.oil:
        //            break;
        //        case ItemType.wood:
        //            break;
        //        case ItemType.rags:
        //            break;
        //        default:
        //            break;
        //    }
        //}
    }
}
