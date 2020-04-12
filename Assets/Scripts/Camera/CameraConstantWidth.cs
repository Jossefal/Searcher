using UnityEngine;

#pragma warning disable 649

public class CameraConstantWidth : MonoBehaviour
{
    [SerializeField] private Camera cameraComponent;
    [SerializeField] private Vector2 defaultResolution;

    private float targetAspect;

    private void Start()
    {
        if (cameraComponent.orthographic)
        {
            targetAspect = defaultResolution.x / defaultResolution.y;

            cameraComponent.orthographicSize *= targetAspect / cameraComponent.aspect;
        }

        enabled = false;
    }
}
