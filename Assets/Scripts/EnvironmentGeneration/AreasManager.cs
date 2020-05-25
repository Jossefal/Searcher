using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649

public class AreasManager : MonoBehaviour
{
    public bool nextAreaIsSpaceman
    {
        get
        {
            if (areasLeftToSpaceman == 0)
            {
                RestartCounters();
                return true;
            }
            else
            {
                areasLeftToSpaceman--;
                return false;
            }
        }
    }

    [SerializeField] private List<Area> unusedEasyAreas;
    [SerializeField] private float startChance;
    [SerializeField] private float chanceDecreaseStep;
    [SerializeField] private float finalChance;
    [SerializeField] private List<Area> unusedNormalAreas;
    [SerializeField] private int minAreasBetweenSpacemans;
    [SerializeField] private int maxAreasBetweenSpacemans;

    private float areasLeftToSpaceman;
    private float easyAreaChance;

    private void Awake()
    {
        RestartCounters();
        easyAreaChance = startChance;
    }

    public void RestartCounters()
    {
        areasLeftToSpaceman = Random.Range(minAreasBetweenSpacemans, maxAreasBetweenSpacemans + 1);
    }

    public Area GetArea()
    {
        Area nextArea;

        if (Random.value <= easyAreaChance)
        {
            nextArea = unusedEasyAreas[Random.Range(0, unusedEasyAreas.Count)];
            unusedEasyAreas.Remove(nextArea);
        }
        else
        {
            nextArea = unusedNormalAreas[Random.Range(0, unusedNormalAreas.Count)];
            unusedNormalAreas.Remove(nextArea);
        }

        easyAreaChance = Mathf.Clamp(easyAreaChance - chanceDecreaseStep, finalChance, 1f);

        return nextArea;
    }

    public void UnuseEasyArea(Area area)
    {
        unusedEasyAreas.Add(area);
    }

    public void UnuseNormalArea(Area area)
    {
        unusedNormalAreas.Add(area);
    }
}
