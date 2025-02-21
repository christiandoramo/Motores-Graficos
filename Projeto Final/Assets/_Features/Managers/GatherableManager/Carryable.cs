using System.Collections.Generic;
using UnityEngine;

public class Carryable : MonoBehaviour
{
    public BoxCollider bc; // para colidir com a Wagon
    public GameObject gatheringPrefab;
    public GameObject playerCarrying;
    public GameObject npcCarrying;
    public int amount;
    public ItemType itemType;

    public void SaveItemAmount(Dictionary<ItemType, Item> inventory)
    {
        if (playerCarrying != null) playerCarrying.GetComponent<GatheringController>().DropCarryable(gameObject);
        //else if (npcCarrying != null) npcCarrying.GetComponent<GatheringController>().NpcDropCarryable();
        inventory[itemType].amount += amount;
    }



    // ao colidir com uma colis�o criada na carro�a o carryable some, e vira um Item 

    // itens podem ser usados para consumo direto - Consumables x Craftables (2 tipos de itens no invent�rio)
    // sistema da tocha e arma deve funcionar semelhante a gatherable/carryable (seria Weaponable)


    // itens podem ser usados para fazer craft
    // crafts: abrigo, tocha, fogueira, arma
    // abrigo - impede morrer no calor ======================== (8 pano, 8 madeira, 2 pedra) = dura 24 horas
    // fogueira - impede morrer no frio ======================= (4 madeira, 2 pedra, 4 oleo) ========= dura 24 horas
    // tocha - personagem segura para iluminar a sua volta ==== (2 pano, 2 madeira, 2 �leo) ========== dura 24 horas
    // arma - derrota npcs com um hit s� ====================== (2 madeira, 2 pedra) ========= dura 5 inimigos morots

    // fruta ========= dura 48 horas
    // �gua  ========= dura 5 dias

    // Lore -
    // pedra na vdd � SUCATA
    // Madeira - Destro�os de casas
    // �leo - �leos nocivos a Terra
    // pano - lona de pl�stico

}
