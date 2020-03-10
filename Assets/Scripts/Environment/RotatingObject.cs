using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    public enum RotatingType
    {
        Rigidbody,
        Transform
    }

    public Vector3 speed;
    public Vector3 rndRadius;
    public bool isRndSpeed;
    public bool isRndRotation;
    public RotatingType rotatingTypeSelection;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if(isRndSpeed)
        {
            speed.x += Random.Range(-rndRadius.x, rndRadius.x);
            speed.y += Random.Range(-rndRadius.y, rndRadius.y);
            speed.z += Random.Range(-rndRadius.z, rndRadius.z);
        }

        if(isRndRotation)
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
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
        // rb.AddTorque(speed.z, ForceMode2D.Force);
        rb.angularVelocity = speed.z;
    }

    private void RotateUsingTransform()
    {  
        // transform.Rotate(speed, Space.World);
        transform.Rotate(speed * Time.fixedDeltaTime, Space.World);
    }
}
