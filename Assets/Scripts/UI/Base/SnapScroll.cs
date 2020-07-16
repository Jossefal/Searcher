using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

#pragma warning disable 649

public class SnapScroll : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private List<Transform> objects = new List<Transform>();
    [SerializeField] private Transform content;
    private RectTransform contentRect;
    [SerializeField] private Transform center;

    private enum Axis
    {
        Vertical,
        Horizontal
    }

    [SerializeField] private Axis axis;
    [SerializeField] private float scrollMultiplier = 1f;
    [SerializeField] [Range(0f, 1f)] private float snapSpeed = 0.1f;
    [SerializeField] private Vector3 normalScale = new Vector3(1f, 1f, 1f);
    [SerializeField] private Vector3 scaleAdd = new Vector3(0.2f, 0.2f, 0.2f);
    [SerializeField] private float scaleDistance = 5f;
    [SerializeField] private Transform startObject;

    private Transform highlightedObject;
    private PointerEventData pointer;
    private Vector3 lastPos;
    private float pointerDownTime;
    private Transform nextHighlightedObject;

    private void Start()
    {
        contentRect = (RectTransform)content;

        objects.Sort((a, b) =>
        {
            if (axis == Axis.Horizontal)
            {
                if (a.position.x > b.position.x)
                    return 1;
                else if (a.position.x < b.position.x)
                    return -1;
                else
                    return 0;
            }
            else
            {
                if (a.position.y > b.position.y)
                    return 1;
                else if (a.position.y < b.position.y)
                    return -1;
                else
                    return 0;
            }
        });

        if (startObject != null)
            SetHighlightedObject(startObject);
    }

    private void Update()
    {
        if (objects.Count == 0)
            return;

        highlightedObject = axis == Axis.Vertical ? GetNearestObjectVertical() : GetNearestObjectHorizontal();

        if (pointer == null)
        {
            Vector3 offset;

            if (nextHighlightedObject != null)
            {
                offset = center.position - nextHighlightedObject.position;

                if (offset.sqrMagnitude < 0.01f)
                    nextHighlightedObject = null;
            }
            else
                offset = center.position - highlightedObject.position;

            content.position = Vector3.Lerp(content.position, content.position + offset, snapSpeed);
        }
    }

    public void SetHighlightedObject(Transform newHighlightedObject)
    {
        if (objects.Contains(newHighlightedObject))
        {
            nextHighlightedObject = null;
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
        nextHighlightedObject = null;
        pointer = pointerEventData;
        lastPos = pointerEventData.pointerPressRaycast.worldPosition;
        pointerDownTime = Time.unscaledTime;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        if (Time.unscaledTime - pointerDownTime < 1f)
        {
            if (axis == Axis.Horizontal)
            {
                if (lastPos.x < pointerEventData.pointerCurrentRaycast.worldPosition.x)
                    nextHighlightedObject = objects[Mathf.Clamp(objects.IndexOf(highlightedObject) - 1, 0, objects.Count - 1)];
                else if (lastPos.x > pointerEventData.pointerCurrentRaycast.worldPosition.x)
                    nextHighlightedObject = objects[Mathf.Clamp(objects.IndexOf(highlightedObject) + 1, 0, objects.Count - 1)];
            }
            else
            {
                if (lastPos.y < pointerEventData.pointerCurrentRaycast.worldPosition.y)
                    nextHighlightedObject = objects[Mathf.Clamp(objects.IndexOf(highlightedObject) - 1, 0, objects.Count - 1)];
                else if (lastPos.y > pointerEventData.pointerCurrentRaycast.worldPosition.y)
                    nextHighlightedObject = objects[Mathf.Clamp(objects.IndexOf(highlightedObject) + 1, 0, objects.Count - 1)];
            }
        }

        pointer = null;
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        Vector3 translation = new Vector3();

        if (axis == Axis.Horizontal)
            translation.x = pointerEventData.pointerCurrentRaycast.worldPosition.x - lastPos.x;
        else
            translation.y = pointerEventData.pointerCurrentRaycast.worldPosition.y - lastPos.y;

        content.Translate(translation * scrollMultiplier);

        lastPos = pointerEventData.pointerCurrentRaycast.worldPosition;
    }
}
