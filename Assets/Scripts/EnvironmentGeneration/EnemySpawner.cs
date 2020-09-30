using System.Collections;
using UnityEngine;

#pragma warning disable 649

public class EnemySpawner : MonoBehaviour
{
    private new Transform transform;


    [SerializeField] private GameObject enemy;
    [SerializeField] private float timeBetween;
    [SerializeField] private float startDelay;
    [SerializeField] private Vector2 spawnRadius;
    [SerializeField] private float enemyLifetime = 20f;

    private void Awake()
    {
        transform = GetComponent<Transform>();
    }

    private void OnEnable()
    {
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        yield return new WaitForSeconds(startDelay);

        while (true)
        {
            Vector3 spawnPos = new Vector3();
            spawnPos.x = Random.Range(transform.position.x - spawnRadius.x, transform.position.x + spawnRadius.x);
            spawnPos.y = Random.Range(transform.position.y - spawnRadius.y, transform.position.y + spawnRadius.y);

            Destroy(Instantiate(enemy, spawnPos, Quaternion.identity), enemyLifetime);

            yield return new WaitForSeconds(timeBetween);
        }
    }
}
