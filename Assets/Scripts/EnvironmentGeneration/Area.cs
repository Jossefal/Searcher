using UnityEngine;

#pragma warning disable 649

public class Area : MonoBehaviour
{
    [SerializeField] private Transform nextAreaSpawnPoint;
    [SerializeField] private AreasChooser areasChooser;
    [SerializeField] private Spawner spacemanSpawner;

    private void Start()
    {
        if(areasChooser.nextAreaIsSpaceman)
            spacemanSpawner.Spawn();
    }

    public void SpawnNextArea()
    {
        GameObject nextArea = areasChooser.GetArea();
        Instantiate(nextArea, nextAreaSpawnPoint.position, nextArea.transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("SpawnTrigger"))
            SpawnNextArea();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + new Vector3(0, 8, 0), new Vector3(9, 100, 0));
    }
}
