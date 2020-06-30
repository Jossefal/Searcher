using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

#pragma warning disable 649

public class SnapScroll : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private List<Transform> objects = new List<Transform>();
    [SerializeField] private Transform content;
    [SerializeField] private Transform center;

    private enum Axis
    {
        Vertical,
        Horizontal
    }

    [SerializeField] private Axis axis;
    [SerializeField] [Range(0f, 1f)] private float snapSpeed = 0.1f;
    [SerializeField] private Vector3 normalScale = new Vector3(1f, 1f, 1f);
    [SerializeField] private Vector3 scaleAdd = new Vector3(0.2f, 0.2f, 0.2f);
    [SerializeField] private float scaleDistance = 5f;
    [SerializeField] private Transform startObject;

    private bool isScrolling;
    private Transform highlightedObject;

    private void Start()
    {
        if (startObject != null)
            SetHighlightedObject(startObject);
    }

    private void Update()
    {
        if (objects.Count == 0)
            return;

        highlightedObject = axis == Axis.Vertical ? GetNearestObjectVertical() : GetNearestObjectHorizontal();

        if (!isScrolling)
        {
            Vector3 offset = center.position - highlightedObject.position;

            content.position = Vector3.Lerp(content.position, content.position + offset, snapSpeed);
        }
    }

    public void SetHighlightedObject(Transform newHighlightedObject)
    {
        if (objects.Contains(newHighlightedObject))
        {
            highlightedObject = newHighlightedObject;
            content.position = content.position + (center.position - highlightedObject.position);
        }
    }

    private Transform GetNearestObjectVertical()
    {
        Transform nearestObject = null;
        float minDistance = float.MaxValue;

        for (int i = 1; i < objects.Count; i++)
        {
            float distance = Mathf.Abs(center.position.y - objects[i].position.y);
            objects[i].localScale = distance == 0f ? normalScale + scaleAdd : normalScale + scaleAdd * (1f - Mathf.Clamp((distance / scaleDistance), 0f, 1f));

            if (distance < minDistance)
            {
                minDistance = distance;
                nearestObject = objects[i];
            }
        }

        return nearestObject;
    }

    private Transform GetNearestObjectHorizontal()
    {
        Transform nearestObject = null;
        float minDistance = float.MaxValue;

        for (int i = 0; i < objects.Count; i++)
        {
            float distance = Mathf.Abs(center.position.x - objects[i].position.x);
            objects[i].localScale = distance == 0f ? normalScale + scaleAdd : normalScale + scaleAdd * (1f - Mathf.Clamp((distance / scaleDistance), 0f, 1f));

            if (distance < minDistance)
            {
                minDistance = distance;
                nearestObject = objects[i];
            }
        }

        return nearestObject;
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        isScrolling = true;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        isScrolling = false;
    }
}
