using System.Collections.Generic;
using UnityEngine;

public class PointsSpawner : Spawner
{
    private List<Transform> points = new List<Transform>();

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            points.Add(transform.GetChild(i));
        }
    }

    private void Start()
    {
        Transform point = points[Random.Range(0, transform.childCount - 1)];        
        Instantiate(spawnObject, new Vector3(Random.Range(point.position.x - xRadius, point.position.x + xRadius), Random.Range(point.position.y - yRadius, point.position.y + yRadius), 0), point.rotation, point);
    }
}
