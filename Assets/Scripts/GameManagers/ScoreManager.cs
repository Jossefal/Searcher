using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int recordScore
    {
        get => _safeRecordScore.GetValue();
    }
    public int currentScore
    {
        get => _safeCurrentScore.GetValue();
    }

    [SerializeField] private Text scoreText = null;
    private SafeInt _safeRecordScore;
    private SafeInt _safeCurrentScore;

    private void Awake()
    {
        _safeRecordScore = new SafeInt(PlayerPrefs.GetInt(Prefs.RECORD_PREF, (int)PlayerPrefs.GetFloat(Prefs.RECORD_PREF, 0f)));
        _safeCurrentScore = new SafeInt(0);
    }

    public void AddPoint()
    {
        _safeCurrentScore++;
        scoreText.text = Converter.ConvertToString(_safeCurrentScore.GetValue());
    }

    public void SendScore()
    {
        if (_safeCurrentScore > _safeRecordScore)
        {
            _safeRecordScore = _safeCurrentScore;
            PlayerPrefs.SetInt(Prefs.RECORD_PREF, _safeRecordScore.GetValue());
        }
    }
}
