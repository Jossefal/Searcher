using UnityEngine;

#pragma warning disable 649

public class Area : MonoBehaviour
{
    [SerializeField] private Transform nextAreaSpawnPoint;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private AreasManager areasManager;
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private Transform[] spacemanSpawnPoints;
    [SerializeField] private GameObject spacemanPrefab;

    private GameObject spaceman;

    public void SpawnNextArea()
    {
        Area nextArea = areasManager.GetArea();
        nextArea.Respawn(new Vector3(cameraTransform.position.x, nextAreaSpawnPoint.position.y, 0f));
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
            areasManager.AddSpaceman(spaceman.transform);
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
        {
            areasManager.RemoveSpaceman(spaceman.transform);
            Destroy(spaceman);
        }

        gameObject.SetActive(false);
    }
}
