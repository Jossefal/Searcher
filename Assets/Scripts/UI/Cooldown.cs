using UnityEngine;
using UnityEngine.Events;

#pragma warning disable 649

public abstract class Cooldown : MonoBehaviour
{
    [SerializeField] protected bool isRealtime;
    [SerializeField] protected UnityEvent onCooldownStart;
    [SerializeField] protected UnityEvent onCooldownEnd;

    protected bool isPaused;

    public abstract void StartCooldown(float time);

    public abstract void StopCooldown();

    public abstract void PauseCooldown();
    public abstract void ResumeCooldown();
}
