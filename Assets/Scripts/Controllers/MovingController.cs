using UnityEngine;

#pragma warning disable 649

public class MovingController : MonoBehaviour
{
    public float speed { get; set; } = 9f;

    [SerializeField] private InterfaceManager interfaceManager;
    [SerializeField] private ControlController controlController;

    private Rigidbody2D rb;
    private StatsController shipStats;

    private const float ROTATION_SPEED = 0.09f;
    private const float MAX_ANGLE = 45;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        shipStats = GetComponent<StatsController>();
    }

    private void Update()
    {
        if (!shipStats.isStunned && !AppManager.isPaused)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, MAX_ANGLE * controlController.Horizontal()), ROTATION_SPEED);
    }

    private void FixedUpdate()
    {
        if (!shipStats.isStunned)
            rb.velocity = transform.up * speed;
        else
            rb.velocity = Vector2.zero;
    }
}
