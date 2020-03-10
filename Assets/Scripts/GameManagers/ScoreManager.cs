using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Transform ship;
    public Text scoreText;
    // public float oneScorePointCost;

    private float recordScore;
    private float currentScore;
    // private float lastPosY;

    private void Awake()
    {
        recordScore = PlayerPrefs.GetFloat(Prefs.RECORD_PREF, 0f);
    }

    // private void Start()
    // {
    //     lastPosY = ship.position.y;
    // }

    // private void Update()
    // {
    //     if (ship.position.y > lastPosY + oneScorePointCost)
    //     {
    //         lastPosY = ship.position.y;
    //         currentScore++;
    //         scoreText.text = Converter.ConvertToString(currentScore);
    //     }
    // }

    public void AddPoint()
    {
        currentScore++;
        scoreText.text = Converter.ConvertToString(currentScore);
    }

    public void SetScore(float newScore)
    {
        currentScore = newScore;
    }

    public float GetScore()
    {
        return currentScore;
    }

    public float GetRecord()
    {
        return recordScore;
    }

    public void SendNewScore(float newScore)
    {
        if (newScore > recordScore)
        {
            PlayerPrefs.SetFloat(Prefs.RECORD_PREF, newScore);
            recordScore = newScore;
        }
    }

    public void SendScore()
    {
        SendNewScore(currentScore);
    }
}
