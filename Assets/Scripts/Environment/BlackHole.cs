using System.Collections;
using UnityEngine;

#pragma warning disable 649

public class BlackHole : MonoBehaviour, IObstacle
{
    [SerializeField] private float pullSpeed = 0.1f;
    [SerializeField] private RespawnPanel respawnPanel;

    private StatsController targetShip;
    private float startDistance;
    private Vector3 scale;

    private void OnTriggerEnter2D(Collider2D col)
    {
        targetShip = col.gameObject.GetComponent<StatsController>();

        targetShip.Stun();
        startDistance = Vector3.Distance(transform.position, targetShip.transform.position);
        StartCoroutine(Pull());
    }

    private IEnumerator Pull()
    {
        Transform transform = this.transform;
        float distancePercent = Vector3.Distance(transform.position, targetShip.transform.position) / startDistance;
        float rotateSpeed = 120f * Mathf.Sign(targetShip.transform.rotation.z);

        while (targetShip != null && distancePercent > 0.1f)
        {
            if (targetShip != null)
            {
                distancePercent = Vector3.Distance(transform.position, targetShip.transform.position) / startDistance;

                if (distancePercent > 0.05f)
                {
                    targetShip.transform.position = Vector3.Lerp(targetShip.transform.position, transform.position, pullSpeed);
                    targetShip.transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);

                    scale.x = scale.y = scale.z = distancePercent;
                    targetShip.transform.localScale = scale;
                }
            }

            yield return null;
        }

        targetShip.gameObject.SetActive(false);
        respawnPanel.Open();
        targetShip = null;
    }

    public void Kill()
    {
        gameObject.SetActive(false);
    }
}
