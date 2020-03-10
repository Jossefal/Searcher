using UnityEngine;

[CreateAssetMenu(fileName = "NewAreasChooser", menuName = "AreasChooser")]
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
    [Header("Battery areas")]
    public bool spawnBatteries = true;
    public GameObject[] batteryAreas;
    // [Range(0f, 1f)] public float batterySpawnChance;
    public int minAreasBetweenBatteries;
    public int maxAreasBetweenBatteries;

    private int areasLeftToBattery;

    [Space]
    [Header("Spaceman areas")]
    public bool spawnSpacemans = true;
    public GameObject[] spacemanAreas;
    public int minAreasBetweenSpacemans;
    public int maxAreasBetweenSpacemans;

    private int areasLeftToSpaceman;

    public void RestartCounters()
    {
        areasLeftToSpaceman = Random.Range(minAreasBetweenSpacemans, maxAreasBetweenSpacemans);
        areasLeftToBattery = Random.Range(minAreasBetweenBatteries, maxAreasBetweenBatteries);

        // batterySpawnChance = Mathf.Clamp(PlayerPrefs.GetFloat("BatterySpawnChance", 0.1f), 0f, 1f);
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
        if(spawnSpacemans && areasLeftToSpaceman <= 0)
        {
            if(spawnBatteries)
                areasLeftToBattery--;

            areasLeftToSpaceman = Random.Range(minAreasBetweenSpacemans, maxAreasBetweenSpacemans);
            return spacemanAreas[Random.Range(0, spacemanAreas.Length)];
        }
        // else if (spawnBatteries && Random.value < batterySpawnChance)
        // {
        //     areasLeftToSpaceman--;
        //     return bonusAreas[Random.Range(0, bonusAreas.Length)];
        // }
        else if(spawnBatteries && areasLeftToBattery <= 0)
        {
            if(spawnSpacemans)
                areasLeftToSpaceman--;

            areasLeftToBattery = Random.Range(minAreasBetweenBatteries, maxAreasBetweenBatteries);
            return batteryAreas[Random.Range(0, batteryAreas.Length)];
        }
        else
        {
            if(spawnSpacemans)
                areasLeftToSpaceman--;
            
            if(spawnBatteries)
                areasLeftToBattery--;

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
}
