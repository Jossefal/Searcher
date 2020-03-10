using UnityEngine;
using UnityEngine.UI;

public class BatteryBar : MonoBehaviour
{
    public BonusController bonusController;
    public Text text;

    public void DisplayBatteries()
    {
        text.text = bonusController.GetCurrentBatteryCount() + "/3";
    }
}
