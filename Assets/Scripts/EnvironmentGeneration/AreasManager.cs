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

    [SerializeField] private List<Area> unusedAreas;
    [SerializeField] private int minAreasBetweenSpacemans;
    [SerializeField] private int maxAreasBetweenSpacemans;

    private float areasLeftToSpaceman;

    private void Awake()
    {
        RestartCounters();
    }

    public void RestartCounters()
    {
        areasLeftToSpaceman = Random.Range(minAreasBetweenSpacemans, maxAreasBetweenSpacemans + 1);
    }

    public Area GetArea()
    {
        Area nextArea = unusedAreas[Random.Range(0, unusedAreas.Count)];
        unusedAreas.Remove(nextArea);
        return nextArea;
    }

    public void UnuseArea(Area area)
    {
        unusedAreas.Add(area);
    }
}
