using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DeathPanel : MonoBehaviour
{
    public Text scoreText;
    public ScoreManager scoreManager;

    [SerializeField] private UnityEvent onOpen = null;

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
