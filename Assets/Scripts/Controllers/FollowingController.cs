using UnityEngine;

#pragma warning disable 649

public class FollowingController : MonoBehaviour
{
    [SerializeField] private Transform pursuedObject;
    [SerializeField] private bool xAxis = true;
    [SerializeField] private bool yAxis = true;
    [SerializeField] private Vector2 offset = new Vector2(0, 0);

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
