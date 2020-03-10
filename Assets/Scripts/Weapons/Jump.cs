using System.Collections;
using UnityEngine;

public class Jump : Weapon
{
    public ShipStats shipsStats;
    public TrailRenderer trail;
    public float speedMultiplier = 2f;
    public float scaleMuiltiplier = 1.5f;
    public float scaleTime = 0.3f;

    private float scaleSpeed;
    private Vector3 resultScale;
    private Vector3 normalScale;

    public override void Activate(float time)
    {
        gameObject.SetActive(true);
        onActivate.Invoke();

        StopAllCoroutines();
        StartCoroutine(Activating(time));
    }

    protected IEnumerator Activating(float time)
    {
        normalScale = shipsStats.transform.localScale;
        resultScale = normalScale * scaleMuiltiplier;
        scaleSpeed = (resultScale - normalScale).magnitude / scaleTime;

        shipsStats.isDamagable = false;
        shipsStats.speed *= speedMultiplier;

        if (bonusPanel != null)
        {
            bonusPanel.gameObject.SetActive(true);
            bonusPanel.StartDurationAnimation(time + scaleTime * 2);
        }

        while (shipsStats.transform.localScale != resultScale)
        {
            shipsStats.transform.localScale = Vector3.MoveTowards(shipsStats.transform.localScale, resultScale, scaleSpeed * Time.deltaTime);
            trail.widthMultiplier = shipsStats.transform.localScale.x / normalScale.x;
            yield return null;
        }

        StartCoroutine(WeaponWorking(time));
    }

    protected IEnumerator Deactivating()
    {
        shipsStats.speed /= speedMultiplier;

        if (bonusPanel != null)
            bonusPanel.gameObject.SetActive(false);

        while (shipsStats.transform.localScale != normalScale)
        {
            shipsStats.transform.localScale = Vector3.MoveTowards(shipsStats.transform.localScale, normalScale, scaleSpeed * Time.deltaTime);
            trail.widthMultiplier = shipsStats.transform.localScale.x / normalScale.x;
            yield return null;
        }

        shipsStats.isDamagable = true;

        onDeactivate.Invoke();
        gameObject.SetActive(false);
    }

    protected override IEnumerator WeaponWorking(float time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(Deactivating());
    }
}
