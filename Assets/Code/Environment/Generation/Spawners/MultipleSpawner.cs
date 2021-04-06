using UnityEngine;

#pragma warning disable 649

public class MultipleSpawner : Spawner
{
    [HideInInspector] public new Transform transform;

    [SerializeField] private Vector2 range; 
    [SerializeField] private int count;

    private void Awake()
    {
        transform = base.transform;
    }   

    void Start()
    {
        if(spawnOnStart)
        {
            for(int i = 0; i < count; i++)
            {
                Vector3 pos = transform.position;
                pos.x += Random.Range(-range.x, range.x);
                pos.y += Random.Range(-range.y, range.y);

                Instantiate(spawnObject, pos, Quaternion.identity, transform.parent);
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, range * 2);
    }
}
