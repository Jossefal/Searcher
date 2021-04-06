using UnityEngine;

#pragma warning disable 649

public class RandomAreaSpawner : MonoBehaviour
{
    public AreasManager areasManager;

    private void Start()
    {
        areasManager.GetArea().Respawn(transform.position);
        
        Destroy(gameObject);
    }
}