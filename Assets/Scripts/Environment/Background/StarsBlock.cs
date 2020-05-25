using UnityEngine;

#pragma warning disable 649

public class StarsBlock : MonoBehaviour
{
    [HideInInspector] public new Transform transform;

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float translateMultiplier;
    [SerializeField] private Transform topPoint;
    [SerializeField] private Transform bottomPoint;
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;
    [SerializeField] private Transform[] stars;
    [SerializeField] private Vector2 range;

    private Vector3 lastPos;
    private Vector3 translatePos = new Vector3(0f, 0f, 0f);

    private void Awake()
    {
        transform = base.transform;
        translatePos = transform.position;
    }

    private void Start()
    {
        lastPos = cameraTransform.position;
        TranslateStars();
    }

    private void Update()
    {
        transform.Translate((lastPos - cameraTransform.position) * translateMultiplier);
        lastPos = cameraTransform.position;

        bool needToTranslate = false;

        if (transform.position.y <= bottomPoint.position.y)
        {
            translatePos.y = topPoint.position.y;
            needToTranslate = true;
        }
        else
            translatePos.y = transform.position.y;

        if (transform.position.x <= leftPoint.position.x)
        {
            translatePos.x = rightPoint.position.x;
            needToTranslate = true;
        }
        else if (transform.position.x >= rightPoint.position.x)
        {
            translatePos.x = leftPoint.position.x;
            needToTranslate = true;
        }
        else
            translatePos.x = transform.position.x;

        if (needToTranslate)
        {
            transform.position = translatePos;
            TranslateStars();
        }
    }

    public void TranslateStars()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            Vector3 pos = transform.position;
            pos.x += Random.Range(-range.x, range.x);
            pos.y += Random.Range(-range.y, range.y);

            stars[i].position = pos;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(base.transform.position, range * 2f);
    }
}
