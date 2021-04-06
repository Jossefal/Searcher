using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649

[CreateAssetMenu(fileName = "NewThemeDataContainer", menuName = "ScriptableObject`s/ThemeDataContainer")]
public class ThemeDataContainer : ScriptableObject
{
    [SerializeField] private ThemeData[] themeDatas;

    private Dictionary<int, ThemeData> themeDataDictionary;

    public void Initialize()
    {
        themeDataDictionary = new Dictionary<int, ThemeData>(themeDatas.Length);

        foreach (ThemeData theme in themeDatas)
        {
            themeDataDictionary[theme.Id] = theme;
        }
    }

    public ThemeData GetThemeData(int id)
    {
        return themeDataDictionary[id];
    }
}
