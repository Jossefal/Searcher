using UnityEngine;

public class RocketThemeChanger : MonoBehaviour
{
    [SerializeField] private ThemeDataContainer themeDataContainer;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private EffectsController effectsController;
    [SerializeField] private Transform trailParent;
    [SerializeField] private SpriteRenderer gunSprite;
    [SerializeField] private Gun gun;

    private void Awake()
    {
        ThemeData currentTheme = themeDataContainer.GetThemeData(DataManager.currentThemeId.GetValue());

        sprite.sprite = currentTheme.RocketSprite;
        effectsController.deathEffectPrefab = currentTheme.RocketDeathEffect;
        gunSprite.sprite = currentTheme.RocketGunSprite;
        gun.BulletPrefab = currentTheme.RocketBulletPrefab;

        Instantiate(currentTheme.RocketTrail, trailParent.position, Quaternion.identity, trailParent);

        this.enabled = false;
    }
}
