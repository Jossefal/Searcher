using UnityEngine;

#pragma warning disable 649

public class RotatingObject : MonoBehaviour
{
    public enum RotatingType
    {
        Rigidbody,
        Transform
    }

    [HideInInspector] public new Transform transform;

    [SerializeField] private Vector3 speed;
    [SerializeField] private Vector3 rndRadius;
    [SerializeField] private bool isRndSpeed;
    [SerializeField] private bool isRndRotation;
    [SerializeField] private bool isRndDirection;
    [SerializeField] private RotatingType rotatingTypeSelection;

    private Rigidbody2D rb;

    private void Awake()
    {
        transform = base.transform;
        rb = GetComponent<Rigidbody2D>();

        if (isRndSpeed)
        {
            speed.x += Random.Range(-rndRadius.x, rndRadius.x);
            speed.y += Random.Range(-rndRadius.y, rndRadius.y);
            speed.z += Random.Range(-rndRadius.z, rndRadius.z);
        }

        if (isRndRotation)
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));

        if (isRndDirection)
            speed *= Random.value > 0.5f ? 1 : -1;
    }

    private void FixedUpdate()
    {
        Rotate();
    }

    private void Rotate()
    {
        switch (rotatingTypeSelection)
        {
            case RotatingType.Rigidbody:
                RotateUsingRigidbody();
                break;
            case RotatingType.Transform:
                RotateUsingTransform();
                break;
        }
    }

    private void RotateUsingRigidbody()
    {
        rb.angularVelocity = speed.z;
    }

    private void RotateUsingTransform()
    {
        transform.Rotate(speed * Time.fixedDeltaTime, Space.World);
    }
}
