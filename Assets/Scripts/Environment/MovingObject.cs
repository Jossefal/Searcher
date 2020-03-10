using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public enum Direction
    {
        Up,
        Right,
        Down,
        Left,
        Custom
    }

    public enum MovingType
    {
        Rigidbody,
        Transform
    }

    public float speed;
    public MovingType movingTypeSelection;
    public Direction directionSelection;
    public Vector3 customDirection;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        customDirection.Normalize();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        switch (movingTypeSelection)
        {
            case MovingType.Rigidbody:
                MoveUsingRigidbody();
                break;
            case MovingType.Transform:
                MoveUsingTransform();
                break;
        }
    }

    private void MoveUsingRigidbody()
    {
        switch (directionSelection)
        {
            case Direction.Up:
                // rb.AddForce(transform.up * speed, ForceMode2D.Force);
                rb.velocity = transform.up * speed;
                break;
            case Direction.Right:
                // rb.AddForce(transform.right * speed, ForceMode2D.Force);
                rb.velocity = transform.right * speed;
                break;
            case Direction.Down:
                // rb.AddForce(-transform.up * speed, ForceMode2D.Force);
                rb.velocity = -transform.up * speed;
                break;
            case Direction.Left:
                // rb.AddForce(-transform.right * speed, ForceMode2D.Force);
                rb.velocity = -transform.right * speed;
                break;
            case Direction.Custom:
                // rb.AddForce(customDirection * speed, ForceMode2D.Force);
                rb.velocity = customDirection * speed;
                break;
        }
    }

    private void MoveUsingTransform()
    {
        switch (directionSelection)
        {
            case Direction.Up:
                transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.up * speed, speed * Time.fixedDeltaTime);
                break;
            case Direction.Right:
                transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.right * speed, speed * Time.fixedDeltaTime);
                break;
            case Direction.Down:
                transform.position = Vector3.MoveTowards(transform.position, transform.position - transform.up * speed, speed * Time.fixedDeltaTime);
                break;
            case Direction.Left:
                transform.position = Vector3.MoveTowards(transform.position, transform.position - transform.right * speed, speed * Time.fixedDeltaTime);
                break;
            case Direction.Custom:
                transform.position = Vector3.MoveTowards(transform.position, transform.position + customDirection * speed, speed * Time.fixedDeltaTime);
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + customDirection);
    }
}
