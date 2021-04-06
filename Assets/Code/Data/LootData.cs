[System.Serializable]
public class LootData
{
    public LootType lootType;
    public int count;

    public override string ToString()
    {
        return lootType + ": " + count;
    }
}

public enum LootType
{
    Diamonds,
    Lives,
}
