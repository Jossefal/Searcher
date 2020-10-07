using UnityEngine;

#pragma warning disable 649

public class ConstantAngle : MonoBehaviour
{
    private new Transform transform;

    [SerializeField] private Vector3 angle;

    private void Awake()
    {
        transform = GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(angle);
    }
}
