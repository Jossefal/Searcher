using System;
using UnityEngine;
using UnityEngine.Events;

#pragma warning disable 649

public class StatsController : MonoBehaviour, IStats
{
    [HideInInspector] public bool isStunned { get; private set; }

    public event Action<int> onHPChanged;

    [SerializeField] protected int maxHp;
    [SerializeField] protected bool isDamagable = true;
    [SerializeField] protected UnityEvent onStunStart;
    [SerializeField] protected UnityEvent onStunEnd;
    [SerializeField] protected UnityEvent onRecieveDamage;
    public UnityEvent onDeath;

    protected int currentHp;

    protected void Awake()
    {
        RestoreHP();
    }

    public void ReceiveDamage(int damage)
    {
        if (!isDamagable)
            return;

        currentHp -= Mathf.Clamp(damage, 0, currentHp);

        onRecieveDamage.Invoke();
        onHPChanged?.Invoke(currentHp);

        if (currentHp == 0)
            Death();
    }

    protected virtual void Death()
    {
        gameObject.SetActive(false);
        onDeath.Invoke();
    }

    public void RestoreHP()
    {
        currentHp = maxHp;

        onHPChanged?.Invoke(currentHp);
    }

    public void Stun()
    {
        isStunned = true;
        onStunStart.Invoke();
    }

    public void StopStun()
    {
        isStunned = false;
        onStunEnd.Invoke();
    }
}
