using UnityEngine;

#pragma warning disable 649

public class RandomSpriteChanger : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }
}
