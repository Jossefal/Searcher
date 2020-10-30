using UnityEngine;

#pragma warning disable 649

[CreateAssetMenu(fileName = "NewMultiThemeData", menuName = "ScriptableObject`s/MultiThemeData")]
public class MultiThemeData : ScriptableObject
{
    [Header("MainInformation")]
    [SerializeField] private int id;
    public int Id { get => id; }

    [SerializeField] private int price;
    public int Price { get => price; }

    [SerializeField] private ValueVariant priceCurrency;
    public ValueVariant PriceCurrency { get => priceCurrency; }

    [SerializeField] private ThemeData[] themes;

    public int ThemesCount { get => themes.Length; }

    public ThemeData GetChildTheme(int index)
    {
        return themes[index];
    }
}
