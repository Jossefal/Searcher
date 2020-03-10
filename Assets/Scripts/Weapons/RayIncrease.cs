using UnityEngine;

public class RayIncrease : MonoBehaviour
{
    public CircleCollider2D magnet;

    private float radiusTmp;

    private void OnEnable()
    {
        radiusTmp = magnet.radius;
        magnet.radius = 8f;
    }

    private void OnDisable()
    {
        magnet.radius = radiusTmp;
    }
}
