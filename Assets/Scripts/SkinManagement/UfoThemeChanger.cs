using UnityEngine;

public class UfoThemeChanger : MonoBehaviour
{
    [SerializeField] private ThemeDataContainer themeDataContainer;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Gun gun;
    [SerializeField] private Transform ufoTrailParent;

    private void Awake()
    {
        ThemeData currentTheme = themeDataContainer.GetThemeData(DataManager.currentThemeId.GetValue());

        sprite.sprite = currentTheme.UfoSprite;
        gun.BulletPrefab = currentTheme.UfoBulletPrefab;

        Instantiate(currentTheme.UfoTrail, ufoTrailParent.position, Quaternion.identity, ufoTrailParent);

        this.enabled = false;
    }
}
