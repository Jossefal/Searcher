using UnityEngine;

#pragma warning disable 649

public class BlackHole : MonoBehaviour
{
    private StatsController targetShip;
    private float startDistance;
    private Vector3 scale;

    private void OnTriggerEnter2D(Collider2D col)
    {
        targetShip = col.gameObject.GetComponent<StatsController>();

        if (targetShip.isGodMod)
        {
            GetComponent<Collider2D>().enabled = false;
            targetShip = null;
            return;
        }

        targetShip.Stun();
        startDistance = Vector3.Distance(transform.position, targetShip.transform.position);
    }

    private void Update()
    {
        if (targetShip != null)
        {
            float distancePercent = Vector3.Distance(transform.position, targetShip.transform.position) / startDistance;
            if (distancePercent > 0.1f)
            {
                scale.x = scale.y = scale.z = distancePercent;
                targetShip.transform.localScale = scale;
            }
            else
            {
                targetShip.GameOver();
                targetShip = null;
            }
        }
    }
}
