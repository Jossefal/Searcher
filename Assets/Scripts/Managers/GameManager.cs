using System.Collections;
using UnityEngine;

#pragma warning disable 649

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject ship;
    [SerializeField] private float startSpeed = 6f;
    [SerializeField] private float speedIncrease = 4f;
    [SerializeField] private float timeToIncrease = 20f;
    [SerializeField] private float increaseTime = 60f;
    [SerializeField] private AnimationCurve[] increaseCurves;

    private AnimationCurve increaseCurve;
    private float startIncreaseTime;
    private Rigidbody2D shipRigidbody;
    private Transform shipTransform;
    private MovingController shipMoving;

    private void Awake()
    {
        shipRigidbody = ship.GetComponent<Rigidbody2D>();
        shipTransform = ship.transform;
        shipMoving = ship.GetComponent<MovingController>();

        ApplyOptions();

        shipMoving.speed = startSpeed;
    }

    private void Start()
    {
        if (increaseTime <= 0f || timeToIncrease < 0f)
            shipMoving.speed = startSpeed + speedIncrease;
        else if (speedIncrease != 0f)
            StartCoroutine(SpeedControl());
    }

    private IEnumerator SpeedControl()
    {
        yield return new WaitForSeconds(timeToIncrease);

        startIncreaseTime = Time.time;

        while (shipMoving.speed != startSpeed + speedIncrease)
        {
            shipMoving.speed = startSpeed + increaseCurve.Evaluate((Time.time - startIncreaseTime) / increaseTime) * speedIncrease;

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
