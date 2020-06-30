using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#pragma warning disable 649

public class DeathPanel : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private UnityEvent onOpen;

    public void Open()
    {
        onOpen.Invoke();
        scoreText.text = Converter.ConvertToString(scoreManager.currentScore);
        gameObject.SetActive(true);
    }

    public void Open(float score, float record)
    {
        onOpen.Invoke();
        scoreText.text = Converter.ConvertToString(score);
        gameObject.SetActive(true);
    }
}
