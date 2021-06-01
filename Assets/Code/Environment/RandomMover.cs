using UnityEngine;

#pragma warning disable 649

public class RandomMover : MonoBehaviour
{
    private new Transform transform;

    [SerializeField] private Vector2 radius;
    [SerializeField] private float speed;
    [SerializeField] private float changePositionDelay;
    [SerializeField] private FollowingController.UpdateType updateType;

    public Vector3 destination { get; private set; }
    public float distance { get; set; }
    private Timer timer = new Timer();

    private void Awake()
    {
        transform = GetComponent<Transform>();

        ChangeDestination();
    }

    private void Update()
    {
        if (updateType == FollowingController.UpdateType.Update)
            DoMovement(Time.deltaTime);
    }

    private void LateUpdate()
    {
        if (updateType == FollowingController.UpdateType.LateUpdate)
            DoMovement(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (updateType == FollowingController.UpdateType.FixedUpdate)
            DoMovement(Time.fixedDeltaTime);
    }

    private void DoMovement(float deltaTime)
    {
        distance = Vector3.Distance(transform.localPosition, destination);

        if (distance > 0.05f)
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, speed * deltaTime);
        else if (!timer.Tick(changePositionDelay))
            ChangeDestination();
    }

    private void ChangeDestination()
    {
        destination = new Vector3(Random.Range(-radius.x, radius.x), Random.Range(-radius.y, radius.y), 0f);
    }
}
