﻿using UnityEngine;

#pragma warning disable 649

public class RandomMover : MonoBehaviour
{
    [HideInInspector] public new Transform transform;

    [SerializeField] private Vector2 radius;
    [SerializeField] private float speed;
    [SerializeField] private FollowingController.UpdateType updateType;

    private Vector3 destination;

    private void Awake()
    {
        transform = GetComponent<Transform>();
        ChangeDestination();
    }

    private void Update()
    {
        if (updateType != FollowingController.UpdateType.Update)
            return;

        if (transform.localPosition != destination)
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, speed * Time.deltaTime);
        else
            ChangeDestination();
    }

    private void LateUpdate()
    {
        if (updateType != FollowingController.UpdateType.LateUpdate)
            return;

        if (transform.localPosition != destination)
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, speed * Time.deltaTime);
        else
            ChangeDestination();
    }

    private void FixedUpdate()
    {
        if (updateType != FollowingController.UpdateType.FixedUpdate)
            return;

        if (transform.localPosition != destination)
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, speed * Time.fixedDeltaTime);
        else
            ChangeDestination();
    }

    private void ChangeDestination()
    {
        destination.x = Random.Range(-radius.x, radius.x);
        destination.y = Random.Range(-radius.y, radius.y);
    }
}
