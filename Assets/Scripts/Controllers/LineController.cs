using UnityEngine;

public class LineController : MonoBehaviour
{
    public Transform begin;
    public Transform end;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (begin == null || end == null)
            Destroy(gameObject);
        else
        {
            lineRenderer.SetPosition(0, begin.position);
            lineRenderer.SetPosition(1, end.position);
        }
    }
}
