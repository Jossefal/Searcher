using System.Collections;
using UnityEngine;

#pragma warning disable 649

public class GameManager : MonoBehaviour
{
    [SerializeField] private StatsController shipStats;
    [SerializeField] private MovingController shipMovingController;
    [SerializeField] private float startSpeed = 7f;
    [SerializeField] private float speedIncrease = 3f;
    [SerializeField] private float timeToIncrease = 30f;
    [SerializeField] private float increaseTime = 180f;
    [SerializeField] private AnimationCurve increaseCurve;
    [SerializeField] private LayerMask obstacleLayers;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private DeathPanel deathPanel;
    [SerializeField] private InterfaceManager interfaceManager;
    [SerializeField] private LevelIntersitialAdUI levelIntersitialAdUI;

    public void Start()
    {
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
            item.GetComponent<IObstacle>().Kill();
        }

        shipStats.gameObject.SetActive(true);
        shipStats.transform.position = new Vector3(0f, shipStats.transform.position.y, 0f);
        shipStats.transform.rotation = new Quaternion(0, 0, 0, 0);
        shipStats.transform.localScale = new Vector3(1f, 1f, 1f);
        shipStats.StopStun();

        interfaceManager.ShowObjects();
    }

    private IEnumerator ShipSpeedControl()
    {
        float timeLeft = timeToIncrease;

        while (timeLeft > 0f)
        {
            if (shipStats.gameObject.activeSelf && !AppManager.isPaused)
                timeLeft -= Time.deltaTime;

            yield return null;
        }

        float timePassed = 0f;

        while (shipMovingController.speed != startSpeed + speedIncrease)
        {
            if (shipStats.gameObject.activeSelf && !AppManager.isPaused)
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
        deathPanel.gameObject.SetActive(true);
        deathPanel.Open();

        if(DataManager.leftToShowAd == 0 && levelIntersitialAdUI.adIsLoaded)
        {
            DataManager.leftToShowAd = DataManager.MAX_LEFT_TO_SHOW_AD;
            StartCoroutine(ShowAdWithDelay(0.5f));
        }
    }

    private IEnumerator ShowAdWithDelay(float time)
    {
        yield return new WaitForSeconds(time);

        DataManager.leftToShowAd = DataManager.MAX_LEFT_TO_SHOW_AD;
        levelIntersitialAdUI.ShowAd(null);
    }

    public void DestroyObject(GameObject destroyingObject)
    {
        Destroy(destroyingObject);
    }
}
