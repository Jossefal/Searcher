using UnityEngine;

public class FollowingController : MonoBehaviour
{
    public Transform pursuedObject;
    public bool xAxis = true;
    public bool yAxis = true;
    public Vector2 offset = new Vector2(0, 0);

    private Vector3 targetPos = new Vector3(0, 0, 0);

    private void Awake()
    {
        targetPos = transform.position;
    }

    private void Update()
    {
        if (pursuedObject != null)
        {
            if (xAxis)
                targetPos.x = pursuedObject.position.x + offset.x;

            if (yAxis)
                targetPos.y = pursuedObject.position.y + offset.y;
        }

        transform.position = targetPos;
    }
}
