using System.Collections;
using UnityEngine;

public class BonusPanel : MonoBehaviour
{
    public RectTransform durationLine;

    private float startBonusTime;
    private float bonusDuration;

    private Vector3 scale = new Vector3(1, 1, 1);

    public void StartDurationAnimation(float bonusDuration)
    {
        this.bonusDuration = bonusDuration;
        startBonusTime = Time.time;
        scale.x = 1;
        durationLine.localScale = scale;
        StopAllCoroutines();
        StartCoroutine(DurationAnimation());
    }

    private IEnumerator DurationAnimation()
    {
        while(Time.time < startBonusTime + bonusDuration)
        {
            scale.x = 1  - (Time.time - startBonusTime)/bonusDuration;
            durationLine.localScale = scale;
            yield return null;
        }
    }
}
