using UnityEngine;

#pragma warning disable 649

public class StarsBlock : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float translateMultiplier;
    [SerializeField] private Transform topPoint;
    [SerializeField] private Transform bottomPoint;
    [SerializeField] private Transform[] stars;
    [SerializeField] private Vector2 range;  

    private Vector3 lastPos;

    private void Start()
    {
        lastPos = cameraTransform.position;
        TranslateStars();
    }

    private void Update()
    {
        if(transform.position == bottomPoint.position)
        {
            transform.position = topPoint.position;
            TranslateStars();
        }

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, bottomPoint.localPosition, (cameraTransform.position.y - lastPos.y) * translateMultiplier);
        lastPos = cameraTransform.position;
    }

    public void TranslateStars()
    {
        for(int i = 0; i < stars.Length; i++)
        {
            Vector3 pos = transform.position;
            pos.x += Random.Range(-range.x, range.x);
            pos.y += Random.Range(-range.y, range.y);

            stars[i].position = pos;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, range * 2);
    }
}
