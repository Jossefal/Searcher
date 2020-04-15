using UnityEngine;

#pragma warning disable 649

public class RandomAreaSpawner : MonoBehaviour
{
    [SerializeField] private AreasChooser areasChooser;

    private void Awake()
    {
        GameObject area = areasChooser.GetArea();
        Instantiate(area, transform.position, area.transform.rotation);
    }
}
