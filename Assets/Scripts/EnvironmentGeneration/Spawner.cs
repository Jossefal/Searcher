using UnityEngine;

#pragma warning disable 649

public class Spawner : MonoBehaviour
{
    [SerializeField] protected GameObject spawnObject;
    [SerializeField] protected float xRadius;
    [SerializeField] protected float yRadius;

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
