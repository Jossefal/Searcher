using UnityEngine;

#pragma warning disable 649

public class Area : MonoBehaviour
{
    [SerializeField] private Transform nextAreaSpawnPoint;
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private Transform[] scalebleObstacles;
    [SerializeField] private Transform[] spacemanSpawnPoints;
    [SerializeField] private GameObject spacemanPrefab;
    [SerializeField] private bool isEasy;

    private AreasManager areasManager;
    private GameObject spaceman;
    private bool nextIsSpawned;

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
        if (areasManager == null)
            areasManager = GameObject.FindWithTag("AreasManager").GetComponent<AreasManager>();

        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].SetActive(true);
        }

        for (int i = 0; i < scalebleObstacles.Length; i++)
        {
            scalebleObstacles[i].localScale = areasManager.currentScale;
            scalebleObstacles[i].gameObject.SetActive(true);
        }

        transform.position = pos;

        gameObject.SetActive(true);
    }

    public void Unuse()
    {
        if (isEasy)
            areasManager.UnuseEasyArea(this);
        else
            areasManager.UnuseNormalArea(this);

        if (spaceman != null)
            Destroy(spaceman);

        nextIsSpawned = false;
        gameObject.SetActive(false);
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
