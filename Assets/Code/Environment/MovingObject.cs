using UnityEngine;

#pragma warning disable 649

public class MovingObject : MonoBehaviour
{
    [HideInInspector] public new Transform transform;
    [HideInInspector] public new Rigidbody2D rigidbody;

    [SerializeField] private Vector3 direction;
    [SerializeField] private bool useTransformUpDirection;
    [SerializeField] private float speed;

    public enum MoveType
    {
        Transform,
        Rigidbody
    }

    [SerializeField] private MoveType moveType;
    [SerializeField] private FollowingController.UpdateType updateType;

    protected void Awake()
    {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody2D>();
        direction.Normalize();
    }

    protected void Update()
    {
        if (updateType != FollowingController.UpdateType.Update)
            return;

        if (moveType == MoveType.Transform)
            transform.position = transform.position + ((useTransformUpDirection ? transform.up : direction) * speed * Time.deltaTime);
        else
            rigidbody.MovePosition(transform.position + ((useTransformUpDirection ? transform.up : direction) * speed * Time.deltaTime));
    }

    protected void FixedUpdate()
    {
        if (updateType != FollowingController.UpdateType.FixedUpdate)
            return;

        if (moveType == MoveType.Transform)
            transform.position = transform.position + ((useTransformUpDirection ? transform.up : direction) * speed * Time.fixedDeltaTime);
        else
            rigidbody.MovePosition(transform.position + ((useTransformUpDirection ? transform.up : direction) * speed * Time.fixedDeltaTime));
    }
}
