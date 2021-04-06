using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LBStore : MonoBehaviour
{
    public void OpenLootBox(LootBox lootBox)
    {
        LootData loot = lootBox.GetLoot();

        Debug.Log(loot);
    }
}
