using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#pragma warning disable 649

public class CooldownPanel : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private bool isRealtime;

    [SerializeField] private UnityEvent onCooldownStart = null;
    [SerializeField] private UnityEvent onCooldownEnd = null;

    public void Open(int seconds)
    {
        onCooldownStart.Invoke();
        StartCoroutine(Cooldown(seconds));
    }

    private IEnumerator Cooldown(int seconds)
    {
        for( ; seconds > 0; seconds--)
        {
            text.text = Converter.ConvertToString(seconds);

            if(isRealtime)
                yield return new WaitForSecondsRealtime(1f);
            else
                yield return new WaitForSeconds(1f);
        }

        text.text = "0";
        onCooldownEnd.Invoke();
    }
}
