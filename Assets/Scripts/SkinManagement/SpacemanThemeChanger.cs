using UnityEngine;

public class SpacemanThemeChanger : MonoBehaviour
{
    [SerializeField] private ThemeDataContainer themeDataContainer;
    [SerializeField] private SpriteRenderer bodySprite;
    [SerializeField] private SpriteRenderer handSprite;

    private void Awake()
    {
        ThemeData currentTheme = themeDataContainer.GetThemeData(DataManager.currentThemeId.GetValue());

        bodySprite.sprite = currentTheme.SpacemanBodySprite;
        handSprite.sprite = currentTheme.SpacemanHandSprite;

        this.enabled = false;
    }
}
