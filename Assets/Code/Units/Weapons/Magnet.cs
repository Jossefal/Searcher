﻿using System.Collections;
using UnityEngine;

#pragma warning disable 649

public class Magnet : MonoBehaviour
{
    [HideInInspector] public new Transform transform;
    [SerializeField] protected GameObject ray;
    [SerializeField] protected float pullSpeed = 0.3f;

    private Transform target;
    private float startDistance;
    private Vector3 scale;

    private void Awake()
    {
        transform = base.transform;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(target != null)
            return;

        target = col.transform;
        target.SetParent(transform);
        startDistance = Vector3.Distance(transform.position, target.position);

        ray.SetActive(true);
        ray.transform.localScale = target.localScale;
        float angle = Vector2.SignedAngle(Vector2.up, target.position - transform.position);
        ray.transform.eulerAngles = new Vector3(0, 0, angle);

        StartCoroutine(Pulling());
    }

    private IEnumerator Pulling()
    {
        while(target != null)
        {
            float scaleValue = Vector3.Distance(transform.position, target.position) / startDistance;
            scale.x = scale.y = scale.z = Mathf.Clamp(scaleValue, 0f, 1f);
            target.localScale = ray.transform.localScale = scale;

            target.position = Vector3.Lerp(target.position, transform.position, pullSpeed);

            yield return null;
        }

        ray.SetActive(false);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        
        if(target != null)
            Destroy(target.gameObject);
        
        ray.SetActive(false);
    }
}
