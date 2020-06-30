using UnityEngine;

#pragma warning disable 649

public class BackgroundBlock : MonoBehaviour
{
    [HideInInspector] public new Transform transform;

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float translateMultiplier;
    [SerializeField] private Transform topPoint;
    [SerializeField] private Transform bottomPoint;
    [SerializeField] private Transform[] objects;
    [SerializeField] private Vector2 range;  

    private Vector3 lastPos;

    private void Awake()
    {
        transform = base.transform;
    }

    private void Start()
    {
        lastPos = cameraTransform.position;
        TranslateObjects();
    }

    private void Update()
    {
        if(transform.position == bottomPoint.position)
        {
            transform.position = topPoint.position;
            TranslateObjects();
        }

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, bottomPoint.localPosition, (cameraTransform.position.y - lastPos.y) * translateMultiplier);
        lastPos = cameraTransform.position;
    }

    public void TranslateObjects()
    {
        for(int i = 0; i < objects.Length; i++)
        {
            Vector3 pos = transform.position;
            pos.x += Random.Range(-range.x, range.x);
            pos.y += Random.Range(-range.y, range.y);

            objects[i].position = pos;
        }
    }
}
