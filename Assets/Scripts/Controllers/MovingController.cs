using UnityEngine;

#pragma warning disable 649

public class MovingController : MonoBehaviour
{
    [SerializeField] private InterfaceManager interfaceManager;
    [SerializeField] private ControlController controlController;

    private float speed = 8f;
    private float rotationSpeed = 0.06f;
    private float maxAngle = 45;  

    private Rigidbody2D rb;
    private StatsController shipStats;

    private void Awake()
    {
        speed = PlayerPrefs.GetFloat("Speed", 8f);
        rotationSpeed = PlayerPrefs.GetFloat("RotationSpeed", 0.06f);
        maxAngle = PlayerPrefs.GetFloat("MaxAngle", 45f);

        float scale = PlayerPrefs.GetFloat("ShipScale", 1f);
        transform.localScale = new Vector3(scale, scale, scale);

        rb = GetComponent<Rigidbody2D>();
        shipStats = GetComponent<StatsController>();
    }

    private void Update()
    {
        if (!shipStats.isStunned && !AppManager.isPaused)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, maxAngle * controlController.Horizontal()), rotationSpeed);
    }

    private void FixedUpdate()
    {
        if (!shipStats.isStunned)
            rb.velocity = transform.up * speed;
        else
            rb.velocity = Vector2.zero;
    }
}
