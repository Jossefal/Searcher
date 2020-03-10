using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void OnEnable()
    {
        text.text = Converter.ConvertToString(PlayerPrefs.GetFloat(Prefs.RECORD_PREF, 0f));
    }
}
