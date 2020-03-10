using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusController : MonoBehaviour
{
    public Weapon[] weapons;
    public int maxBatteryCount = 3;
    public BatteryBar batteryBar;
    public Weapon invulnerability;
    public float invulnerabilityTime;
    public CooldownPanel invulnerabilityCooldown;

    private int currentBatteryCount;

    private Dictionary<string, Weapon> weaponsContainer = new Dictionary<string, Weapon>();

    private void Awake()
    {
        for(int i = 0; i < weapons.Length; i++)
        {
            weaponsContainer.Add(weapons[i].weaponName, weapons[i]);
        }

        batteryBar.DisplayBatteries();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Bonus")
        {   
            Bonus bonus = coll.gameObject.GetComponent<Bonus>();
            weaponsContainer[bonus.weaponName].Activate(bonus.time);
            Destroy(bonus.gameObject);
        }
    }

    public void AddBattery(int count)
    {
        currentBatteryCount += Mathf.Clamp(count, 0, maxBatteryCount - currentBatteryCount);

        if(currentBatteryCount == maxBatteryCount)
        {
            ReduceBatteryCount(maxBatteryCount);
            invulnerability.Activate(invulnerabilityTime);
            invulnerabilityCooldown.Open((int) invulnerabilityTime);
        }

        batteryBar.DisplayBatteries();
    }

    public void ReduceBatteryCount(int count)
    {
        currentBatteryCount -= Mathf.Clamp(count, 0, currentBatteryCount);

        batteryBar.DisplayBatteries();
    }

    public int GetCurrentBatteryCount()
    {
        return currentBatteryCount;
    }
}
