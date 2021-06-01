using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649

public class AreasManager : MonoBehaviour
{
    public RespawnPanel respawnPanel
    {
        get
        {
            return _respawnPanel;
        }
    }

    public bool isSkyforceMode { get; set; }

    [SerializeField] private RespawnPanel _respawnPanel;
    [SerializeField] private List<Area> unusedEasyAreas;
    [SerializeField] private List<Area> unusedNormalAreas;

    [SerializeField] private GameData gameData;

    [Space]
    [Header("Difficulty oprions")]
    [SerializeField] private float startEasyChance;
    [SerializeField] private float easyChanceDecreaseStep;
    [SerializeField] private float finalEasyChance;

    private float easyAreaChance;
    private PickableManager pickableManager;

    private void Awake()
    {
        startEasyChance = gameData.StartEasyChance;
        easyChanceDecreaseStep = gameData.EasyChanceDecreaseStep;
        finalEasyChance = gameData.FinalEasyChance;

        pickableManager = GetComponent<PickableManager>();
        pickableManager.Initialize();

        easyAreaChance = startEasyChance;

        InitializeAreas();
    }

    private void InitializeAreas()
    {
        foreach (Area area in unusedEasyAreas)
        {
            area.Initialize(this);
        }

        foreach (Area area in unusedNormalAreas)
        {
            area.Initialize(this);
        }
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

        pickableManager.ProccesCounters();

        if(pickableManager.QueryNotEmpty())
            nextArea.SpawnObject(pickableManager.GetPickable());

        return nextArea;
    }

    public void ReturnToPool(Area area)
    {
        if(area.IsEasy)
            unusedEasyAreas.Add(area);
        else
            unusedNormalAreas.Add(area);
    }
}
