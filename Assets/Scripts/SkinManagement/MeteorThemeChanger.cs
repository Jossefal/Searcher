using UnityEngine;

public class MeteorThemeChanger : MonoBehaviour
{
    [SerializeField] private ThemeDataContainer themeDataContainer;
    [SerializeField] private Transform meteor;
    [SerializeField] private EffectsController effectsController;

    private void Awake()
    {
        ThemeData currentTheme = themeDataContainer.GetThemeData(DataManager.currentThemeId.GetValue());

        Instantiate(currentTheme.MeteorSprite, meteor.position, Quaternion.identity, meteor);

        effectsController.deathEffectPrefab = currentTheme.MeteorDeathEffect;

        this.enabled = false;
    }
}
