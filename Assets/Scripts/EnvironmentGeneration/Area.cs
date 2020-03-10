using UnityEngine;

public class Area : MonoBehaviour
{
    public Transform nextAreaSpawnPoint;
    public AreasChooser areasChooser;

    public void SpawnNextArea()
    {
        Instantiate(areasChooser.GetArea(), nextAreaSpawnPoint.position, nextAreaSpawnPoint.rotation);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("SpawnTrigger"))
            SpawnNextArea();
    }
}
