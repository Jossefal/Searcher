using System.Collections;
using UnityEngine;

#pragma warning disable 649

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject ship;
    [SerializeField] private float startSpeed = 7f;
    [SerializeField] private float speedIncrease = 2f;
    [SerializeField] private float timeToIncrease = 5f;
    [SerializeField] private float increaseTime = 20f;
    [SerializeField] private AnimationCurve increaseCurve;
    [SerializeField] private LayerMask obstacleLayers;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private DeathPanel deathPanel;
    [SerializeField] private InterfaceManager interfaceManager;

    private MovingController shipMovingController;

    public void Start()
    {
        shipMovingController = ship.GetComponent<MovingController>();

        shipMovingController.speed = startSpeed;
        if (speedIncrease > 0f)
            StartCoroutine(ShipSpeedControl());
    }

    public void RespawnShip()
    {
        Camera camera = Camera.main;
        Vector2 box = new Vector2(camera.orthographicSize * camera.aspect * 2f, 16f);
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(camera.transform.position, box, 0f, obstacleLayers);

        foreach (Collider2D item in collider2Ds)
        {
            item.GetComponent<Obstacle>().Kill();
        }

        ship.SetActive(true);
        ship.transform.position = new Vector3(0f, ship.transform.position.y, 0f);
        ship.transform.rotation = new Quaternion(0, 0, 0, 0);

        interfaceManager.ShowObjects();
    }

    private IEnumerator ShipSpeedControl()
    {
        float timeLeft = timeToIncrease;

        while (timeLeft > 0f)
        {
            if (ship.activeSelf && !AppManager.isPaused)
                timeLeft -= Time.deltaTime;

            yield return null;
        }

        float timePassed = 0f;

        while (shipMovingController.speed != startSpeed + speedIncrease)
        {
            if (ship.activeSelf && !AppManager.isPaused)
            {
                shipMovingController.speed = startSpeed + increaseCurve.Evaluate(timePassed / increaseTime) * speedIncrease;
                timePassed += Time.deltaTime;
            }

            yield return null;
        }
    }

    public void GameOver()
    {
        scoreManager.SendScore();
        interfaceManager.HideObjects();
        deathPanel.Open();
    }

    public void DestroyObject(GameObject destroyingObject)
    {
        Destroy(destroyingObject);
    }
}
