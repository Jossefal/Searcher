using UnityEngine;

#pragma warning disable 649

public class MovingController : MonoBehaviour
{
    public float speed { get; set; } = 9f;
    [HideInInspector] public new Transform transform;

    [SerializeField] private InterfaceManager interfaceManager;
    [SerializeField] private InputController controlController;

    private Rigidbody2D rb;
    private StatsController shipStats;

    private const float ROTATION_SPEED = 0.09f;
    private const float MAX_ANGLE = 45;

    private void Awake()
    {
        transform = base.transform;
        rb = GetComponent<Rigidbody2D>();
        shipStats = GetComponent<StatsController>();
    }

    private void Update()
    {
        if (!shipStats.isStunned && !AppManager.isPaused)
            DetermineRotation();
    }

    private void FixedUpdate()
    {
        DoMovement();
    }

    private void DetermineRotation()
    {
        Quaternion targetRotation = Quaternion.Euler(0, 0, MAX_ANGLE * controlController.Horizontal());
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, ROTATION_SPEED);
    }

    private void DoMovement()
    {
        if (!shipStats.isStunned)
            rb.velocity = CalculateVelocity();
        else
            rb.velocity = Vector2.zero;
    }

    private Vector2 CalculateVelocity()
    {
        float angleB = Vector3.Angle(Vector3.up, transform.up);
        float angleA = 90f - angleB;
        float a = speed;
        float b = a * Mathf.Tan(angleB * Mathf.Deg2Rad);

        float sign = Mathf.Sign(Vector3.SignedAngle(Vector3.up, transform.up, Vector3.forward));
        return new Vector2(b * -sign, a);
    }
}
