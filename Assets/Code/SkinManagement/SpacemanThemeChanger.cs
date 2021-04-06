using UnityEngine;

#pragma warning disable 649

public class SpacemanThemeChanger : MonoBehaviour
{
    [SerializeField] private ThemeDataContainer themeDataContainer;
    [SerializeField] private SpriteRenderer bodySprite;
    [SerializeField] private SpriteRenderer[] handSprites;

    private void Awake()
    {
        ThemeData currentTheme = themeDataContainer.GetThemeData(DataManager.currentThemeId.GetValue());

        bodySprite.sprite = currentTheme.SpacemanBodySprite;

        if (handSprites != null)
        {
            handSprites[0].sprite = currentTheme.SpacemanHandSprite;
            handSprites[1].sprite = currentTheme.SpacemanHandSprite;
        }

        this.enabled = false;
    }
}
