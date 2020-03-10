using UnityEngine;

public class MultipleSpawner : MonoBehaviour
{
    public GameObject spawnObject;
    public int count;
    public float xRadius;
    public float yRadius;

    private void Awake()
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(spawnObject, new Vector3(Random.Range(transform.position.x - xRadius, transform.position.x + xRadius), Random.Range(transform.position.y - yRadius, transform.position.y + yRadius), transform.position.z), transform.rotation, transform.parent);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(xRadius * 2, yRadius * 2, 0));
    }
}