using System.Collections;
using UnityEngine;

#pragma warning disable 649

public class MovingController : MonoBehaviour
{
    [SerializeField] private InterfaceManager interfaceManager;
    [SerializeField] private ControlController controlController;
    [SerializeField] private float startSpeed = 6f;
    [SerializeField] private float speedIncrease = 4f;
    [SerializeField] private float timeToIncrease = 20f;
    [SerializeField] private float increaseTime = 60f;
    [SerializeField] private AnimationCurve increaseCurve;

    private float speed = 9f;
    private const float rotationSpeed = 0.09f;
    private const float maxAngle = 45;  

    private Rigidbody2D rb;
    private StatsController shipStats;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        shipStats = GetComponent<StatsController>();
    }

    private void Start()
    {
        speed = startSpeed;
        StartCoroutine(SpeedControl());
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

    private IEnumerator SpeedControl()
    {
        yield return new WaitForSeconds(timeToIncrease);

        float startIncreaseTime = Time.time;

        while (speed != startSpeed + speedIncrease)
        {
            speed = startSpeed + increaseCurve.Evaluate((Time.time - startIncreaseTime) / increaseTime) * speedIncrease;

            yield return null;
        }
    }
}
