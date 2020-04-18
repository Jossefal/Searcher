using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 649

public class KeyPanel : MonoBehaviour
{
    [SerializeField] private float time = 5f;
    [SerializeField] private Text keyCountText;
    [SerializeField] private Button useKeyBtn;
    [SerializeField] private Cooldown cooldown;
    [SerializeField] private GameManager gameManager;

    public void Open()
    {
        int keyCount = DataManager.keyCount.GetValue();
        keyCountText.text = Converter.ConvertToString(DataManager.keyCount.GetValue());
        if (keyCount == 0)
            useKeyBtn.interactable = false;

        gameObject.SetActive(true);
        cooldown?.StartCooldown(time);
    }

    public void Close()
    {
        gameObject.SetActive(false);
        gameManager.GameOver();
    }

    public void UseKey()
    {
        if (DataManager.keyCount.GetValue() > 0)
        {
            DataManager.keyCount--;
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
