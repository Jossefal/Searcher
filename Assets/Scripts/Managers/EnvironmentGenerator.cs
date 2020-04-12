using UnityEngine;

#pragma warning disable 649

public class EnvironmentGenerator : MonoBehaviour
{
    [SerializeField] private AreasChooser areasChooser;

    private void Awake()
    {
        areasChooser.RestartCounters();
    }
}
