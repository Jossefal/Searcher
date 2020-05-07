using UnityEngine;

#pragma warning disable 649

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnObjects;

    void Start()
    {
        Instantiate(spawnObjects[Random.Range(0, spawnObjects.Length)], transform.position, Quaternion.identity);
    }
}
