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

    public List<Transform> spacemans = new List<Transform>();

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

    public void AddSpaceman(Transform spaceman)
    {
        spacemans.Add(spaceman);
    }

    public void RemoveSpaceman(Transform spaceman)
    {
        spacemans.Remove(spaceman);
    }

    public Transform GetNearestSpaceman(Vector3 pos)
    {
        if (spacemans.Count == 0)
            return null;

        Transform nearestSpaceman = spacemans[0];
        float minDistance = Vector3.Distance(pos, nearestSpaceman.position);

        for (int i = 1; i < spacemans.Count; i++)
        {
            float currentDistance = Vector3.Distance(pos, spacemans[i].position);
            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                nearestSpaceman = spacemans[i];
            }
        }

        return nearestSpaceman;
    }
}
