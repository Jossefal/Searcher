using UnityEngine;
using UnityEngine.Events;

public class StatsController : MonoBehaviour
{
    public float damage;
    public float maxHp;
    public bool isDamagable = true;
    [SerializeField] protected UnityEvent onDamaged = null;
    [SerializeField] protected UnityEvent onDeath = null;

    protected float currentHp;
    

    private void Awake()
    {
        RestoreHP();
    }

    public virtual void ReceiveDamage(float damage)
    {
        if (!isDamagable)
            return;

        currentHp -= Mathf.Clamp(damage, 0, currentHp);
        onDamaged.Invoke();
        if (currentHp == 0)
            Death();
    }

    public virtual void Death()
    {
        onDeath.Invoke();
    }

    public void SetMaxHp(float newHp)
    {
        maxHp = Mathf.Abs(newHp);
    }

    public void RestoreHP()
    {
        currentHp = maxHp;
    }

    public void SetCurrentHp(float newHp)
    {
        currentHp = Mathf.Clamp(newHp, 0, maxHp);
    }

    public float GetCurrentHp()
    {
        return currentHp;
    }

    public float GetCurrentPercentOfHp()
    {
        return currentHp / maxHp;
    }
}
