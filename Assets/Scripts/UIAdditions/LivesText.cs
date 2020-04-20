using UnityEngine;
using UnityEngine.UI;

public class LivesText : MonoBehaviour
{
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void OnEnable()
    {
        Show();
    }

    public void Show()
    {
        text.text = Converter.ConvertToString(DataManager.livesCount.GetValue());
    }
}
