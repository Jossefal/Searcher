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

    public RespawnPanel respawnPanel
    {
        get
        {
            return _respawnPanel;
        }
    }

    [HideInInspector] public Vector3 currentScale;

    [SerializeField] private RespawnPanel _respawnPanel;
    [SerializeField] private List<Area> unusedEasyAreas;
    [SerializeField] private List<Area> unusedNormalAreas;
    [SerializeField] private int minAreasBetweenSpacemans;
    [SerializeField] private int maxAreasBetweenSpacemans;

    [Space]
    [Header("Difficulty oprions")]
    [SerializeField] private float startEasyChance;
    [SerializeField] private float easyChanceDecreaseStep;
    [SerializeField] private float finalEasyChance;
    [SerializeField] private float startScale;
    [SerializeField] private uint areasToScale;
    [SerializeField] private float scaleStep;
    [SerializeField] private float finalScale;

    private float areasLeftToSpaceman;
    private float easyAreaChance;

    private void Awake()
    {
        RestartCounters();
        easyAreaChance = startEasyChance;
        currentScale = new Vector3(startScale, startScale, startScale);
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

        easyAreaChance = Mathf.Clamp(easyAreaChance - easyChanceDecreaseStep, finalEasyChance, 1f);

        if (areasToScale != 0)
            areasToScale--;
        else if (currentScale.x < finalScale)
            currentScale.x = currentScale.y = Mathf.Clamp(currentScale.x + scaleStep, 1, finalScale);

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
