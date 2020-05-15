using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 649

public class ScoreManager : MonoBehaviour
{
    public int currentScore
    {
        get => safeCurrentScore.GetValue();
    }

    [SerializeField] private Text scoreText = null;

    private SafeInt safeCurrentScore;

    private void Awake()
    {
        safeCurrentScore = new SafeInt(0);
    }

    public void AddPoint()
    {
        safeCurrentScore++;
        scoreText.text = Converter.ConvertToString(safeCurrentScore.GetValue());
    }

    public void SendScore()
    {
        if (safeCurrentScore > DataManager.record)
        {
            DataManager.record = safeCurrentScore;

            // if(GPGSManager.isAuthenticated)
            //     GPGSManager.ReportScore(safeCurrentScore.GetValue());
        }
    }
}
