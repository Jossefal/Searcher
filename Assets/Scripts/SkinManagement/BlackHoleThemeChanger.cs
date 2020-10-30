using UnityEngine;

public class BlackHoleThemeChanger : MonoBehaviour
{
    [SerializeField] private ThemeDataContainer themeDataContainer;
    [SerializeField] private Transform blackHole;

    private void Awake()
    {
        ThemeData currentTheme = themeDataContainer.GetThemeData(DataManager.currentThemeId.GetValue());

        Instantiate(currentTheme.BlackHoleSprite, blackHole.position, Quaternion.identity, blackHole);

        this.enabled = false;
    }
}
