using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] protected GameObject spawnObject;
    [SerializeField] protected bool spawnOnStart;

    private void Start()
    {
        if(spawnOnStart)
            Spawn();
    }

    public virtual void Spawn()
    {
        Instantiate(spawnObject, transform.position, spawnObject.transform.rotation, transform.parent);

        Destroy(gameObject);
    }
}