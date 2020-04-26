using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 649

public class LivesText : MonoBehaviour
{
    [SerializeField] private Text text;

    private void OnEnable()
    {
        Show();
    }

    public void Show()
    {
        text.text = Converter.ConvertToString(DataManager.livesCount.GetValue());
    }
}
