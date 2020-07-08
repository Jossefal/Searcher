using System.Collections;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 649

public class CooldownPanel : Cooldown
{
    [SerializeField] private Text text;

    public override void StartCooldown(float time)
    {
        onCooldownStart.Invoke();
        StartCoroutine(Cooldown((int)time));
    }

    private IEnumerator Cooldown(float seconds)
    {
        for (; seconds > 0; seconds--)
        {
            if (!isPaused)
            {
                text.text = Converter.ConvertToString((int)seconds);

                if (isRealtime)
                    yield return new WaitForSecondsRealtime(1f);
                else
                    yield return new WaitForSeconds(1f);
            }
            else
                yield return null;
        }

        text.text = "0";
        onCooldownEnd.Invoke();
    }

    public override void StopCooldown()
    {
        StopAllCoroutines();
        text.text = "0";
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
