using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    public string weaponName;
    public BonusPanel bonusPanel;

    protected bool isBonusActive;

    [SerializeField] protected UnityEvent onActivate = null;
    [SerializeField] protected UnityEvent onDeactivate = null;

    public virtual void Activate(float time)
    {
        isBonusActive = true;
        onActivate.Invoke();
        gameObject.SetActive(true);

        if (bonusPanel != null)
        {
            bonusPanel.gameObject.SetActive(true);
            bonusPanel.StartDurationAnimation(time);
        }
        
        StopAllCoroutines();
        StartCoroutine(WeaponWorking(time));
    }

    public void Deactivate()
    {
        if(!isBonusActive)
            return;
        
        StopAllCoroutines();
        onDeactivate.Invoke();
        gameObject.SetActive(false);
        
        if (bonusPanel != null)
            bonusPanel.gameObject.SetActive(false);
    }

    protected virtual IEnumerator WeaponWorking(float time)
    {
        yield return new WaitForSeconds(time);
        Deactivate();
    }
}
