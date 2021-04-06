using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 649

public class LootPanel : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private Image _icon;
    [SerializeField] private ParticleSystem particleEffect;

    public string text
    {
        get => _text != null ? _text.text : "";
        set
        {
            if (_text != null)
                _text.text = value;
        }
    }

    public Sprite icon
    {
        get => _icon != null ? _icon.sprite : null;
        set
        {
            if (_icon != null)
                _icon.sprite = value;
        }
    }
}
