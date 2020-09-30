﻿using System.Collections.Generic;
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

    [HideInInspector] public Vector3 currentScale;

    [SerializeField] private RespawnPanel _respawnPanel;
    [SerializeField] private List<Area> unusedEasyAreas;
    [SerializeField] private List<Area> unusedNormalAreas;

    [Space]
    [Header("Difficulty oprions")]
    [SerializeField] private float startEasyChance;
    [SerializeField] private float easyChanceDecreaseStep;
    [SerializeField] private float finalEasyChance;
    [SerializeField] private float startScale;
    [SerializeField] private uint areasToScale;
    [SerializeField] private float scaleStep;
    [SerializeField] private float finalScale;

    private float easyAreaChance;
    private PickableManager pickableManager;

    private void Awake()
    {
        pickableManager = GetComponent<PickableManager>();
        pickableManager.Initialize();

        easyAreaChance = startEasyChance;
        currentScale = new Vector3(startScale, startScale, startScale);

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

        if (areasToScale != 0)
            areasToScale--;
        else if (currentScale.x < finalScale)
            currentScale.x = currentScale.y = Mathf.Clamp(currentScale.x + scaleStep, 1, finalScale);

        pickableManager.Evaluate(nextArea);

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
