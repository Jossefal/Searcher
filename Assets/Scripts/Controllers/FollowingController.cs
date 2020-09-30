using UnityEngine;

#pragma warning disable 649

public class FollowingController : MonoBehaviour
{
    [HideInInspector] public new Transform transform;

    [SerializeField] private Transform pursuedObject;
    [SerializeField] private bool xAxis = true;
    [SerializeField] private bool yAxis = true;
    [SerializeField] private Vector2 offset = new Vector2(0, 0);

    public enum UpdateType
    {
        Update,
        LateUpdate,
        FixedUpdate
    }

    [SerializeField] private UpdateType updateType;

    private Vector3 targetPos = new Vector3(0, 0, 0);

    private void Awake()
    {
        transform = base.transform;
        targetPos = transform.position;
    }

    private void Update()
    {
        if (updateType == UpdateType.Update)
            Move();
    }

    private void LateUpdate()
    {
        if (updateType == UpdateType.LateUpdate)
            Move();
    }

    private void FixedUpdate()
    {
        if (updateType == UpdateType.FixedUpdate)
            Move();
    }

    private void Move()
    {
        if (pursuedObject != null)
        {
            if (xAxis)
                targetPos.x = pursuedObject.position.x + offset.x;

            if (yAxis)
                targetPos.y = pursuedObject.position.y + offset.y;
        }

        transform.position = targetPos;
    }
}
