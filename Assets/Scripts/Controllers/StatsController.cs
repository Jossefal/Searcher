using UnityEngine;
using UnityEngine.Events;

#pragma warning disable 649

public class StatsController : MonoBehaviour, IStats
{
    [HideInInspector] public bool isStunned { get; private set; }

    [SerializeField] private float maxHp;
    [SerializeField] private bool isDamagable = true;
    [SerializeField] private UnityEvent onStunStart;
    [SerializeField] private UnityEvent onStunEnd;
    [SerializeField] private UnityEvent onDeath;

    private float currentHp;

    private void Awake()
    {
        RestoreHP();
    }

    public void ReceiveDamage(float damage)
    {
        if (!isDamagable)
            return;

        currentHp -= Mathf.Clamp(damage, 0, currentHp);
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
