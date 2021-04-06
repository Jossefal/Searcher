using UnityEngine;

#pragma warning disable 649

public class RandomPointSpawner : Spawner
{
    [SerializeField] private Transform[] points;

    private void Start()
    {
        if(spawnOnStart)
            Spawn();
    }

    public override void Spawn()
    {
        Instantiate(spawnObject, points[Random.Range(0, points.Length)].position, spawnObject.transform.rotation, transform.parent);

        Destroy(gameObject);
    }
}
