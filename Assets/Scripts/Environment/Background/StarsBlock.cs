using UnityEngine;

public class StarsBlock : MonoBehaviour
{
    public Transform cameraTransform;
    public float translateMultiplier;
    public Transform topPoint;
    public Transform bottomPoint;

    private Vector3 lastPos;

    private void Start()
    {
        lastPos = cameraTransform.position;
    }

    private void Update()
    {
        if(transform.position == bottomPoint.position)
            transform.position = topPoint.position;

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, bottomPoint.localPosition, (cameraTransform.position.y - lastPos.y) * translateMultiplier);
        lastPos = cameraTransform.position;
    }
}
