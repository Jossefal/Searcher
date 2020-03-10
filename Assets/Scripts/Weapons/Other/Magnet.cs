using System.Collections;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public GameObject rayLine;
    public GameObject lightEffect;
    public float pullSpeed = 0.3f;
    public Animator doorAnimator;

    private Transform target;
    private float startDistance;
    private Vector3 scale;

    private Vector3 lastPos;

    private LineRenderer rayLineRenderer;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(target != null)
            return;

        target = col.transform;
        startDistance = Vector3.Distance(transform.position, target.position);

        Instantiate(lightEffect, target.position, target.rotation, target);
        rayLineRenderer = Instantiate(rayLine, transform.position, Quaternion.identity, transform).GetComponent<LineRenderer>();

        lastPos = transform.position;

        StartCoroutine(Pulling());
    }

    private IEnumerator Pulling()
    {
        doorAnimator.SetTrigger("openTrigger");

        while(target != null)
        {
            scale.x = scale.y = scale.z = Mathf.Clamp(Vector3.Distance(transform.position, target.position) / startDistance, 0f, 1f);
            target.localScale = scale;

            target.position += (transform.position - lastPos);
            target.position = Vector3.Lerp(target.position, transform.position, pullSpeed);

            lightEffect.transform.position = target.position;

            rayLineRenderer.SetPosition(0, transform.position);
            rayLineRenderer.SetPosition(1, target.position);

            lastPos = transform.position;

            yield return null;
        }

        Destroy(rayLineRenderer.gameObject);
        doorAnimator.SetTrigger("closeTrigger");
    }
}
