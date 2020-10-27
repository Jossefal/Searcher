using System;
using System.Collections;
using UnityEngine;

#pragma warning disable 649

public class EnemySpawner : MonoBehaviour
{
    private new Transform transform;

    [System.Serializable]
    public class EnemyType
    {
        public int difficultyLevel;
        public GameObject prefab;
    }

    [SerializeField] private EnemyType[] enemyTypes;
    [SerializeField] private SkyforceModeManager skyforceModeManager;
    [SerializeField] private float spawnDelay = 5f;

    private GameObject enemyController;
    private Obstacle enemy;

    private void Awake()
    {
        transform = GetComponent<Transform>();

        Array.Sort<EnemyType>(enemyTypes, (x, y) =>
        {
            if (x.difficultyLevel < y.difficultyLevel)
                return -1;
            else if (x.difficultyLevel > y.difficultyLevel)
                return 1;
            else
                return 0;
        });
    }

    private EnemyType GetEnemyType()
    {
        if (enemyTypes.Length == 0)
            return null;

        int returnedObject = 0;

        for (int i = 1; i < enemyTypes.Length; i++)
        {
            if (enemyTypes[i].difficultyLevel > skyforceModeManager.difficultyLevel)
                break;

            if (enemyTypes[i].difficultyLevel > enemyTypes[returnedObject].difficultyLevel)
                returnedObject = i;
        }

        return enemyTypes[returnedObject];
    }

    public void SpawnEnemy(Action onEnemyDeath)
    {
        StartCoroutine(EnemySpawning(onEnemyDeath));
    }

    public void KillEnemy()
    {
        enemy?.Kill();
    }

    private IEnumerator EnemySpawning(Action onEnemyDeath)
    {
        yield return new WaitForSeconds(spawnDelay);

        EnemyType enemyType = GetEnemyType();

        enemyController = Instantiate(enemyType.prefab, transform.position, Quaternion.identity, transform);

        enemy = enemyController.GetComponentInChildren<Obstacle>();

        enemy.onDeath.AddListener(new UnityEngine.Events.UnityAction(onEnemyDeath));
        enemy.onDeath.AddListener(new UnityEngine.Events.UnityAction(DestroyEnemyController));
    }

    private void DestroyEnemyController()
    {
        if (enemyController != null)
            Destroy(enemyController);
    }
}
