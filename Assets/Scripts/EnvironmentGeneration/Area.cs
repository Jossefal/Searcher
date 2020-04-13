using UnityEngine;

#pragma warning disable 649

public class Area : MonoBehaviour
{
    [SerializeField] private Transform nextAreaSpawnPoint;
    [SerializeField] private AreasChooser areasChooser;

    public void SpawnNextArea()
    {
        Instantiate(areasChooser.GetArea(), nextAreaSpawnPoint.position, nextAreaSpawnPoint.rotation);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("SpawnTrigger"))
            SpawnNextArea();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + new Vector3(0, 8, 0), new Vector3(9, 16, 0));
    }
}
