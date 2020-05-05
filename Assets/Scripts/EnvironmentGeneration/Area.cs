using UnityEngine;

#pragma warning disable 649

public class Area : MonoBehaviour
{
    [SerializeField] private Transform nextAreaSpawnPoint;
    [SerializeField] private AreasManager areasManager;
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private Transform[] spacemanSpawnPoints;
    [SerializeField] private GameObject spacemanPrefab;

    private GameObject spaceman;

    public void SpawnNextArea()
    {
        Area nextArea = areasManager.GetArea();
        nextArea.Respawn(nextAreaSpawnPoint.position);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("SpawnTrigger"))
            SpawnNextArea();
        else if (coll.CompareTag("UnuseTrigger"))
            Unuse();
    }

    private void OnEnable()
    {
        if (areasManager.nextAreaIsSpaceman)
        {
            int pointIndex = Random.Range(0, spacemanSpawnPoints.Length);
            spaceman = Instantiate(spacemanPrefab, spacemanSpawnPoints[pointIndex].position, Quaternion.identity, transform);
        }
    }

    public void Respawn(Vector3 pos)
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].SetActive(true);
        }

        transform.position = pos;

        gameObject.SetActive(true);
    }

    public void Unuse()
    {
        areasManager.UnuseArea(this);

        if (spaceman != null)
            Destroy(spaceman);

        gameObject.SetActive(false);
    }
}
