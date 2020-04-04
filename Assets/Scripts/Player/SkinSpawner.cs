using UnityEngine;

public class SkinSpawner : MonoBehaviour
{
    public SkinsContainer skinsContainer;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer.sprite = skinsContainer.GetCurrentSkin();
        Destroy(gameObject);
    }
}
