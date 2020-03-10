using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ShipStats : StatsController
{
    public float speed = 6f;
    public bool isStunned;
    public InterfaceManager interfaceManager;
    [SerializeField] private UnityEvent onStunStart = null;
    [SerializeField] private UnityEvent onStunEnd = null;
    [SerializeField] private UnityEvent onGameOver = null;

    [HideInInspector] public bool isGodMod;

    private bool isGameOver;

    private void Awake()
    {
        ApplyOptions();
        RestoreHP();
    }

    public override void ReceiveDamage(float damage)
    {
        if (isGodMod || !isDamagable)
            return;

        currentHp -= Mathf.Clamp(damage, 0, currentHp);
        onDamaged.Invoke();
        if (currentHp == 0)
            Death();
    }

    public void GameOver()
    {
        if(isGameOver)
            return;
        
        isGameOver = true;
        onGameOver.Invoke();
    }

    public override void Death()
    {
        onDeath.Invoke();
        GameOver();
    }

    public void Stun()
    {
        isStunned = true;
        onStunStart.Invoke();
    }

    public void Stun(float time)
    {
        StartCoroutine(StunCoroutine(time));
    }

    public void StopStunning()
    {
        StopCoroutine("StunCoroutine");
        isStunned = false;
        onStunEnd.Invoke();
    }

    private IEnumerator StunCoroutine(float time)
    {
        isStunned = true;
        onStunStart.Invoke();
        yield return new WaitForSeconds(time);
        isStunned = false;
        onStunEnd.Invoke();
    }

    private void ApplyOptions()
    {
        isGodMod = interfaceManager.GetBoolPref("IsGodMode", false);
    }
}
