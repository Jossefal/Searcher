using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour
{
    public AreasChooser areasChooser;

    private void Awake()
    {
        areasChooser.RestartCounters();
    }
}
