using System.Collections;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 649

public class CooldownLine : Cooldown
{
    [SerializeField] private RectTransform line;
    
    public override void StartCooldown(float time)
    {
        onCooldownStart.Invoke();
        StartCoroutine(Cooldown(time));
    }

    private IEnumerator Cooldown(float time)
    {
        float timeLeft = time;
        float scale = 1f;
        Vector3 localScale = new Vector3(1f, 1f, 1f);

        while (timeLeft > 0)
        {
            if (!isPaused)
            {
                scale = timeLeft / time;
                localScale.x = scale;
                line.localScale = localScale;

                timeLeft -= isRealtime ? Time.unscaledDeltaTime : Time.deltaTime;
            }

            yield return null;
        }

        onCooldownEnd.Invoke();
    }

    public override void StopCooldown()
    {
        StopAllCoroutines();
    }

    public override void PauseCooldown()
    {
        isPaused = true;
    }

    public override void ResumeCooldown()
    {
        isPaused = false;
    }
}
