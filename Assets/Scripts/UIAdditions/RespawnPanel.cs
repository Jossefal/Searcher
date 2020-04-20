using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 649

public class RespawnPanel : MonoBehaviour
{
    [SerializeField] private float time = 5f;
    [SerializeField] private Text livesCountText;
    [SerializeField] private Button useLifeBtn;
    [SerializeField] private Cooldown cooldown;
    [SerializeField] private GameManager gameManager;

    public void Open()
    {
        int keyCount = DataManager.livesCount.GetValue();
        livesCountText.text = Converter.ConvertToString(keyCount);
        if (keyCount == 0)
            useLifeBtn.interactable = false;

        gameObject.SetActive(true);
        cooldown?.StartCooldown(time);
    }

    public void Close()
    {
        gameObject.SetActive(false);
        gameManager.GameOver();
    }

    public void UseLife()
    {
        if (DataManager.livesCount.GetValue() > 0)
        {
            DataManager.livesCount--;
            Respawn();
        }
    }

    public void Respawn()
    {
        gameObject.SetActive(false);
        cooldown?.StopCooldown();
        gameManager.RespawnShip();
    }
}
