using UnityEngine;

#pragma warning disable 649

[CreateAssetMenu(fileName = "NewAreasChooser", menuName = "ScriptableObject`s/AreasChooser")]
public class AreasChooser : ScriptableObject
{
    [Space]
    [Header("Normal areas")]
    public GameObject[] areas;
    public float[] chances;

    public enum ChooseType
    {
        withChances,
        random
    }
    public ChooseType chooseType;

    [Space]
    [Header("Spacemans")]
    public bool spawnSpacemans = true;
    public uint minAreasBetweenSpacemans;
    public uint maxAreasBetweenSpacemans;
    public bool nextAreaIsSpaceman
    {
        get
        {
            if (spawnSpacemans && areasLeftToSpaceman == 0)
            {
                RestartCounters();
                return true;
            }
            else if(spawnSpacemans)
            {
                areasLeftToSpaceman--;
                return false;
            }
            else
                return false;
        }
    }

    private int areasLeftToSpaceman;

    public void RestartCounters()
    {
        areasLeftToSpaceman = Random.Range((int)minAreasBetweenSpacemans, (int)maxAreasBetweenSpacemans + 1);
    }

    private int ChooseIndex()
    {
        if (areas.Length == 1)
            return 0;
        float total = 0;
        foreach (float elem in chances)
        {
            total += elem;
        }
        float randomValue = Random.value * total;
        for (int i = 0; i < chances.Length; i++)
        {
            if (randomValue < chances[i])
            {
                return i;
            }
            else
            {
                randomValue -= chances[i];
            }
        }
        return chances.Length - 1;
    }

    public GameObject GetArea()
    {
        switch (chooseType)
        {
            case ChooseType.withChances:
                return areas[ChooseIndex()];
            case ChooseType.random:
                return areas[Random.Range(0, areas.Length)];
            default:
                return null;
        }
    }
}
