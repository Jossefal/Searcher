using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 649

public class VersionText : MonoBehaviour
{
    [SerializeField] private Text text;

    private void OnEnable()
    {
        Show();
    }

    public void Show()
    {
        text.text = "version " + Application.version;
    }
}
