using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 649

public class ScoreText : MonoBehaviour
{
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void OnEnable()
    {
        text.text = Converter.ConvertToString(DataManager.record.GetValue());
    }
}
