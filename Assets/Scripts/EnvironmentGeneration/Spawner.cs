using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] protected GameObject spawnObject;
    [SerializeField] protected Vector2 objectSize;
    [SerializeField] protected Vector2 range;

    private void Start()
    {
        Spawn();
    }

    public virtual void Spawn()
    {
        Vector2 spawnRange = new Vector2();
        spawnRange.x = range.x - objectSize.x/2f;
        spawnRange.y = range.y - objectSize.y/2f;

        Vector3 spawnPos = new Vector3();
        spawnPos.x = Random.Range(transform.position.x - spawnRange.x, transform.position.x + spawnRange.x);
        spawnPos.y = Random.Range(transform.position.y - spawnRange.y, transform.position.y + spawnRange.y);

        Instantiate(spawnObject, spawnPos, Quaternion.identity, transform.parent);

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(range.x * 2, range.y * 2, 0));
    }
}
