using System.Collections;
using UnityEngine;

#pragma warning disable 649

public class SkyforceModeManager : MonoBehaviour
{
    public int difficultyLevel { get; private set; }

    private AreasManager areasManager;

    [SerializeField] private GameData gameData;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private float timeBetween;
    [SerializeField] private GameObject shipGun;
    [SerializeField] private float gunActivationDelay = 3f;

    private void Awake()
    {
        timeBetween = gameData.TimeBetweenSkyforce;

        areasManager = GetComponent<AreasManager>();

        StartCoroutine(ModeActivation());
    }

    private IEnumerator ModeActivation()
    {
        yield return new WaitForSeconds(timeBetween);

        areasManager.isSkyforceMode = true;
        difficultyLevel++;
        enemySpawner.SpawnEnemy(DeactivateMode);

        if (!shipGun.activeInHierarchy)
            Invoke("SetActiveShipGun", 3f);
    }

    public void DeactivateMode()
    {
        areasManager.isSkyforceMode = false;
        shipGun.SetActive(false);

        StartCoroutine(ModeActivation());
    }

    private void SetActiveShipGun()
    {
        shipGun.SetActive(!shipGun.activeInHierarchy);
    }
}
