using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnObject;
    public float xRadius;
    public float yRadius;

    private void Start()
    {
        Instantiate(spawnObject, new Vector3(Random.Range(transform.position.x - xRadius, transform.position.x + xRadius), Random.Range(transform.position.y - yRadius, transform.position.y + yRadius), 0), transform.rotation, transform.parent);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(xRadius * 2, yRadius * 2, 0));
    }
}
