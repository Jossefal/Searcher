using UnityEngine;

#pragma warning disable 649

public class SpriteContainer : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;

    private RandomObjectsArray<Sprite> spritesArray;

    private void Awake()
    {
        spritesArray = new RandomObjectsArray<Sprite>(sprites);
    }

    public Sprite GetSprite()
    {
        return spritesArray.Next();
    }
}
