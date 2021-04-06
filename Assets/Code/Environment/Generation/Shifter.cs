using UnityEngine;

#pragma warning disable 649

public class Shifter : MonoBehaviour
{
    [SerializeField] private Vector3 range;

    private void Start()
    {
        transform.position += new Vector3(Random.Range(-range.x, range.x), Random.Range(-range.y, range.y), Random.Range(-range.z, range.z));
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(range.x * 2, range.y * 2, 0));
    }
}
