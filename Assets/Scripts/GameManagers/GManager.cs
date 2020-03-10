using System.Collections;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public GameObject ship;
    public float startSpeed = 6f;
    public float speedIncrease = 4f;
    public float timeToIncrease = 20f;
    public float increaseTime = 60f;
    public AnimationCurve[] increaseCurves;

    private AnimationCurve increaseCurve;

    private float startIncreaseTime;

    // public float startDistance;
    // public float speedIncreaseDisapnce;
    // public float speedIncreaseStep;
    // public float maxSpeed;

    // private Vector3 lastPos;
    // private float distanceLeft;

    private Rigidbody2D shipRigidbody;
    private Transform shipTransform;
    private ShipStats shipStats;

    private float minVelocity = 0.5f;

    private void Awake()
    {
        shipRigidbody = ship.GetComponent<Rigidbody2D>();
        shipTransform = ship.transform;
        shipStats = ship.GetComponent<ShipStats>();

        ApplyOptions();

        shipStats.speed = startSpeed;

        // distanceLeft = startDistance;
        // lastPos = shipTransform.position;
    }

    private void Start()
    {
        if(increaseTime <= 0f || timeToIncrease < 0f)
            shipStats.speed = startSpeed + speedIncrease;
        else if(speedIncrease != 0f)
            StartCoroutine(SpeedControl());
    }

    private void FixedUpdate()
    {
        if (ship != null && shipRigidbody.velocity.magnitude < minVelocity)
            shipStats.GameOver();
    }

    private void Update()
    {
        // if(shipStats.speed >= maxSpeed)
        //     return;

        // if(distanceLeft <= 0)
        // {
        //     shipStats.speed += speedIncreaseStep;
        //     distanceLeft = speedIncreaseDisapnce;
        // }
        // else
        //     distanceLeft -= shipTransform.position.y - lastPos.y;

        // lastPos = shipTransform.position;
    }

    private IEnumerator SpeedControl()
    {
        yield return new WaitForSeconds(timeToIncrease);

        startIncreaseTime = Time.time;

        while (shipStats.speed != startSpeed + speedIncrease)
        {
            shipStats.speed = startSpeed + increaseCurve.Evaluate((Time.time - startIncreaseTime) / increaseTime) * speedIncrease;

            yield return null;
        }
    }

    public void DestroyObject(GameObject destroyingObject)
    {
        Destroy(destroyingObject);
    }

    private void ApplyOptions()
    {
        startSpeed = PlayerPrefs.GetFloat("StartSpeed", 6f);
        speedIncrease = PlayerPrefs.GetFloat("SpeedIncrease", 8f);
        startIncreaseTime = PlayerPrefs.GetFloat("TimeToIncrease", 20f);
        increaseTime = PlayerPrefs.GetFloat("IncreaseTime", 160f);
        increaseCurve = increaseCurves[PlayerPrefs.GetInt("IncreaseCurve", 0)];
    }
}
