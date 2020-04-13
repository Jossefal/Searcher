using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 649

public class KeyPanel : MonoBehaviour
{
    [SerializeField] private int seconds = 5;
    [SerializeField] private Text keyCountText;
    [SerializeField] CooldownPanel cooldownPanel;
    [SerializeField] StatsController shipStats;
    [SerializeField] GameManager gameManager;

    public void Open()
    {
        gameObject.SetActive(true);
        cooldownPanel?.Open(seconds);
    }

    public void Close()
    {
        gameObject.SetActive(false);
        gameManager.GameOver();
    }

    public void Respawn()
    {
        gameObject.SetActive(false);
        cooldownPanel.Stop();
        gameManager.RespawnShip();
    }
}
