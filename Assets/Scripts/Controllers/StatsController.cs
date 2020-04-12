using UnityEngine;
using UnityEngine.Events;

#pragma warning disable 649

public class StatsController : MonoBehaviour
{
    [HideInInspector] public bool isGodMod { get; private set; }
    [HideInInspector] public bool isStunned { get; private set; }

    [SerializeField] private float maxHp;
    [SerializeField] private bool isDamagable = true;
    [SerializeField] private UnityEvent onStunStart;
    [SerializeField] private UnityEvent onStunEnd;
    [SerializeField] private UnityEvent onGameOver;

    private float currentHp;
    private bool isGameOver;

    private void Awake()
    {
        ApplyOptions();
        RestoreHP();
    }

    public void ReceiveDamage(float damage)
    {
        if (isGodMod || !isDamagable)
            return;

        currentHp -= Mathf.Clamp(damage, 0, currentHp);
        if (currentHp == 0)
            Death();
    }

    public void GameOver()
    {
        if (isGameOver)
            return;

        isGameOver = true;
        onGameOver.Invoke();
    }

    private void Death()
    {
        GameOver();
        Destroy(gameObject);
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

    private void ApplyOptions()
    {
        isGodMod = Prefs.GetBoolPref("IsGodMode", false);
    }
}
