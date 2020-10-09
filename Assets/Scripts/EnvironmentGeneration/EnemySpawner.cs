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
    [SerializeField] private float timeBetween;
    [SerializeField] private float startDelay;
    [SerializeField] private Vector2 spawnRadius;
    [SerializeField] private float enemyLifetime = 20f;
    [SerializeField] private SkyforceModeManager skyforceModeManager;

    private float activateTime;
    private float activeTime;

    private void Awake()
    {
        transform = GetComponent<Transform>();
    }

    public void Activate(float activeTime)
    {
        activateTime = Time.time;
        this.activeTime = activeTime;
        StartCoroutine(Spawning());
        Invoke("Deactivate", activeTime);
    }

    private void Deactivate()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawning()
    {
        yield return new WaitForSeconds(startDelay);

        EnemyType enemyType = GetEnemyType();

        while (true)
        {
            Vector3 spawnPos = new Vector3();
            spawnPos.x = Random.Range(transform.position.x - spawnRadius.x, transform.position.x + spawnRadius.x);
            spawnPos.y = Random.Range(transform.position.y - spawnRadius.y, transform.position.y + spawnRadius.y);

            EnemyController newEnemy = Instantiate(enemyType.prefab, spawnPos, Quaternion.identity).GetComponent<EnemyController>();
            newEnemy.SetLifetime(activateTime + activeTime - Time.time);

            Destroy(newEnemy.gameObject, enemyLifetime);

            yield return new WaitForSeconds(timeBetween);
        }
    }

    private EnemyType GetEnemyType()
    {
        for (int i = 0; i < enemyTypes.Length; i++)
        {
            if (enemyTypes[i].difficultyLevel == skyforceModeManager.difficultyLevel)
                return enemyTypes[i];
        }

        return null;
    }
}
