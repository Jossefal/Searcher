using UnityEngine;

public class CameraConstantWidth : MonoBehaviour
{
    public Camera cameraComponent;
    public Vector2 defaultResolution;

    private float targetAspect;

    private void Start()
    {
        if (cameraComponent.orthographic)
        {
            targetAspect = defaultResolution.x / defaultResolution.y;

            cameraComponent.orthographicSize *= targetAspect / cameraComponent.aspect;
        }
    }
}
