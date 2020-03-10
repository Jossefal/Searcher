using UnityEngine;

public class MovingController : MonoBehaviour
{
    public float rotatingSpeed = 0.3f;
    public uint maxAngle = 45;
    public InterfaceManager interfaceManager;
    public ControlController controlController;
    
    private Rigidbody2D rb;
    private ShipStats shipStats;

    private float rotation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        shipStats = GetComponent<ShipStats>();
    }

    private void Update()
    {
        if (!shipStats.isStunned && !interfaceManager.GetOpenMenusStatus())
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, maxAngle * controlController.Horizontal()), rotatingSpeed);
    }

    private void FixedUpdate()
    {
        if (!shipStats.isStunned)
            rb.velocity = transform.up * shipStats.speed;
        else
            rb.velocity = Vector2.zero;
    }
}
