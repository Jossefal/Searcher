using UnityEngine;

public class BatteryRestorer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        BonusController bonusController = col.GetComponent<BonusController>();

        if(bonusController != null)
        {
            bonusController.AddBattery(1);
            Destroy(gameObject);
        }
    }
}
