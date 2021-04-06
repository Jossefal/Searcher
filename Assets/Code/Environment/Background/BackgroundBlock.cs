using UnityEngine;

#pragma warning disable 649

public class BackgroundBlock : MonoBehaviour
{
    [HideInInspector] public new Transform transform;

    [SerializeField] protected Transform cameraTransform;
    [SerializeField] protected float translateMultiplier;
    [SerializeField] protected Transform topPoint;
    [SerializeField] protected Transform bottomPoint;

    protected Vector3 lastPos;

    protected void Awake()
    {
        transform = base.transform;
    }

    protected void Start()
    {
        lastPos = cameraTransform.position;
        OnStart();
        OnTranslate();
    }

    protected void Update()
    {
        Move();
    }

    private void Move()
    {
        if (transform.position == bottomPoint.position)
        {
            transform.position = topPoint.position;
            OnTranslate();
        }

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, bottomPoint.localPosition, (cameraTransform.position.y - lastPos.y) * translateMultiplier);
        lastPos = cameraTransform.position;
    }

    protected virtual void OnStart()
    {

    }

    protected virtual void OnTranslate()
    {

    }
}
