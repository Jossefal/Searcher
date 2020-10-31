using UnityEngine;

#pragma warning disable 649

public class BackgroundThemeChanger : MonoBehaviour
{
    [SerializeField] private ThemeDataContainer themeDataContainer;
    [SerializeField] private Transform parent;

    private void Awake()
    {
        ThemeData currentTheme = themeDataContainer.GetThemeData(DataManager.currentThemeId.GetValue());

        Instantiate(currentTheme.BackgroundPrefab, parent.position, Quaternion.identity, parent);

        this.enabled = false;
    }
}
