using UnityEngine;

public class Star : MonoBehaviour
{
    public SpriteRenderer sprite;

    private void Awake()
    {
        int r = Mathf.Clamp(PlayerPrefs.GetInt("R", 255), 0, 255);
        int g = Mathf.Clamp(PlayerPrefs.GetInt("G", 255), 0, 255);
        int b = Mathf.Clamp(PlayerPrefs.GetInt("B", 255), 0, 255);
        sprite.color = new Color(r, g, b, sprite.color.a);
    }    
}