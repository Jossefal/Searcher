using UnityEngine;

#pragma warning disable 649

public class MovingController : MonoBehaviour
{
    [SerializeField] private InterfaceManager interfaceManager;
    [SerializeField] private ControlController controlController;

    private const float speed = 8f;
    private const float rotatingSpeed = 0.06f;
    private const uint maxAngle = 45;  

    private Rigidbody2D rb;
    private StatsController shipStats;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        shipStats = GetComponent<StatsController>();
    }

    private void Update()
    {
        if (!shipStats.isStunned && !interfaceManager.GetOpenMenusStatus())
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, maxAngle * controlController.Horizontal()), rotatingSpeed);
    }

    private void FixedUpdate()
    {
        if (!shipStats.isStunned)
            rb.velocity = transform.up * speed;
        else
            rb.velocity = Vector2.zero;
    }
}
