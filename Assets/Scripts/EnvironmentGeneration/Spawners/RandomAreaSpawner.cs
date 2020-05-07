using UnityEngine;

#pragma warning disable 649

public class RandomAreaSpawner : MonoBehaviour
{
    [SerializeField] private AreasManager areasManager;

    private void Start()
    {
        areasManager.GetArea().Respawn(transform.position);
        gameObject.SetActive(false);
    }
}