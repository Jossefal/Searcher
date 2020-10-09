using System;
using UnityEngine;
using UnityEngine.Events;

#pragma warning disable 649

public class StatsController : MonoBehaviour, IStats
{
    [HideInInspector] public bool isStunned { get; private set; }

    public event Action<int> onHPChanged;

    [SerializeField] private int maxHp;
    [SerializeField] private bool isDamagable = true;
    [SerializeField] private UnityEvent onStunStart;
    [SerializeField] private UnityEvent onStunEnd;
    [SerializeField] private UnityEvent onRecieveDamage;
    [SerializeField] private UnityEvent onDeath;
    private int currentHp;

    private void Awake()
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

    private void Death()
    {
        gameObject.SetActive(false);
        onDeath.Invoke();
    }

    public void RestoreHP()
    {
        currentHp = maxHp;
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
