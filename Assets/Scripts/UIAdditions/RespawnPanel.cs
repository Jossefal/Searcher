using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 649

public class RespawnPanel : MonoBehaviour
{
    [SerializeField] private float time = 5f;
    [SerializeField] private int livesCost = 1;
    [SerializeField] private int costMultiplier = 2;
    [SerializeField] private Text livesCostText;
    [SerializeField] private Text livesCountText;
    [SerializeField] private Button useLifeBtn;
    [SerializeField] private Cooldown cooldown;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Cooldown respawnCooldown;

    public void Open()
    {
        int livesCount = DataManager.livesCount.GetValue();
        livesCountText.text = Converter.ConvertToString(livesCount);
        livesCostText.text = Converter.ConvertToString(livesCost);
        if (livesCount < livesCost)
            useLifeBtn.interactable = false;

        gameObject.SetActive(true);
        cooldown.ResumeCooldown();
        cooldown?.StartCooldown(time);
    }

    public void Close()
    {
        gameObject.SetActive(false);
        gameManager.GameOver();
    }

    public void UseLife()
    {
        if (DataManager.livesCount.GetValue() >= livesCost)
        {
            DataManager.livesCount = new SafeInt(DataManager.livesCount.GetValue() - livesCost);
            livesCost *= costMultiplier;
            Respawn();
        }
    }

    public void Respawn()
    {
        gameObject.SetActive(false);
        cooldown?.StopCooldown();
        gameManager.RespawnShip();
    }

    public void RespawnAfterCooldown(float time)
    {
        gameObject.SetActive(false);
        cooldown?.StopCooldown();
        respawnCooldown.StartCooldown(3f);
    }

    public void PauseCooldown()
    {
        cooldown.PauseCooldown();
    }

    public void ResumeCooldown()
    {
        cooldown.ResumeCooldown();
    }
}
