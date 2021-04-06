using UnityEngine;

#pragma warning disable 649

[CreateAssetMenu(fileName = "NewLootBox", menuName = "ScriptableObject`s/LootBox")]
public class LootBox : ScriptableObject
{
    [System.Serializable]
    public class Loot
    {
        public float weight;
        public LootData lootData;
    }

    [SerializeField] private Loot[] loot;

    public LootData GetLoot()
    {
        if (loot.Length == 0)
            return null;

        System.Array.Sort(loot, (x, y) =>
        {
            if (x.weight < y.weight)
                return 1;
            else if (x.weight > y.weight)
                return -1;
            else
                return 0;
        });

        float total = 0;
        foreach (Loot current in loot)
            total += current.weight;

        float randomPoint = total * Random.value;

        for (int i = 0; i < loot.Length; i++)
        {
            if (randomPoint < loot[i].weight)
                return loot[i].lootData;

            randomPoint -= loot[i].weight;
        }

        return loot[loot.Length - 1].lootData;
    }
}
