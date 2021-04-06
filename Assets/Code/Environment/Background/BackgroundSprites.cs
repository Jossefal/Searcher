using UnityEngine;

#pragma warning disable 649

public class BackgroundSprites : BackgroundBlock
{
    [SerializeField] private SpriteContainer spriteContainer;

    private SpriteRenderer spriteRenderer;

    protected override void OnStart()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void OnTranslate()
    {
        spriteRenderer.sprite = spriteContainer.GetSprite();
    }
}
