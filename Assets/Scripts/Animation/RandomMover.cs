using UnityEngine;

#pragma warning disable 649

public class RandomMover : MonoBehaviour
{
    [HideInInspector] public new Transform transform;

    [SerializeField] private Vector2 radius;
    [SerializeField] private float speed;
    [SerializeField] private float changePositionDelay;
    [SerializeField] private FollowingController.UpdateType updateType;

    private Vector3 destination;
    private Timer timer = new Timer();

    private void Awake()
    {
        transform = GetComponent<Transform>();

        ChangeDestination();
    }

    private void Update()
    {
        if (updateType != FollowingController.UpdateType.Update)
            return;

        if (Vector3.Distance(transform.localPosition, destination) > 0.05f)
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, speed * Time.deltaTime);
        else if (!timer.Tick(changePositionDelay))
            ChangeDestination();
    }

    private void LateUpdate()
    {
        if (updateType != FollowingController.UpdateType.LateUpdate)
            return;

        if (Vector3.Distance(transform.localPosition, destination) > 0.05f)
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, speed * Time.deltaTime);
        else if (!timer.Tick(changePositionDelay))
            ChangeDestination();
    }

    private void FixedUpdate()
    {
        if (updateType != FollowingController.UpdateType.FixedUpdate)
            return;

        if (Vector3.Distance(transform.localPosition, destination) > 0.05f)
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, speed * Time.deltaTime);
        else if (!timer.Tick(changePositionDelay))
            ChangeDestination();
    }

    private void ChangeDestination()
    {
        destination.x = Random.Range(-radius.x, radius.x);
        destination.y = Random.Range(-radius.y, radius.y);
    }
}
