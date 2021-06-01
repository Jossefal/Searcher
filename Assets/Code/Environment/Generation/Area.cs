using UnityEngine;

#pragma warning disable 649

public class Area : MonoBehaviour
{
    private new Transform transform;

    [SerializeField] private Transform nextAreaSpawnPoint;
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private Transform spawnPointsParent;
    [SerializeField] private bool isEasy;

    public bool IsEasy => isEasy;

    private AreasManager areasManager;
    private GameObject spawnedObject;
    private bool nextIsSpawned;
    private Transform[] spawnPoints;

    public void Initialize(AreasManager manager)
    {
        this.areasManager = manager;
        transform = GetComponent<Transform>();

        if (spawnPointsParent.childCount != 0)
        {
            spawnPoints = new Transform[spawnPointsParent.childCount];

            for (int i = 0; i < spawnPointsParent.childCount; i++)
            {
                spawnPoints[i] = spawnPointsParent.GetChild(i);
            }
        }
    }

    public void SpawnNextArea()
    {
        Area nextArea = areasManager.GetArea();
        nextArea.Respawn(nextAreaSpawnPoint.position);
        nextIsSpawned = true;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("SpawnTrigger") && !nextIsSpawned)
            SpawnNextArea();
        else if (coll.CompareTag("UnuseTrigger"))
            Decommission();
    }

    public void SpawnObject(GameObject prefab)
    {
        if (spawnedObject == null)
        {
            int pointIndex = Random.Range(0, spawnPoints.Length);
            Vector3 position = spawnPoints[pointIndex].position;
            spawnedObject = Instantiate(prefab, position, Quaternion.identity, transform);
        }
    }

    public void Respawn(Vector3 pos)
    {
        SetObstaclesActive(!areasManager.isSkyforceMode);
        
        transform.position = pos;

        gameObject.SetActive(true);
    }

    public void Decommission()
    {
        areasManager.ReturnToPool(this);

        if (spawnedObject != null)
            Destroy(spawnedObject);

        nextIsSpawned = false;
        gameObject.SetActive(false);
    }

    private void SetObstaclesActive(bool active)
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].SetActive(active);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 pos = base.transform.position;
        Vector3 cubeSize = new Vector3(9f, nextAreaSpawnPoint.position.y - pos.y, 0f);
        Vector3 cubePos = pos;
        cubePos.y += cubeSize.y / 2f;
        Gizmos.DrawWireCube(cubePos, cubeSize);
    }
}
