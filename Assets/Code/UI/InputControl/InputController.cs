using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

#pragma warning disable 649

public class InputController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float maxOffsetX = 1f;

    [SerializeField] private Transform ship;
    [SerializeField] private Camera mainCamera;

    private PointerEventData pointer;
    private List<PointerEventData> pointers = new List<PointerEventData>();

    public void OnPointerDown(PointerEventData ped)
    {
        pointers.Add(ped);
    }

    public void OnPointerUp(PointerEventData ped)
    {
        pointers.Remove(ped);
    }

    public float Horizontal()
    {
        if (pointers.Count == 0)
            return 0f;

        Vector2 pointerWorldPos = mainCamera.ScreenToWorldPoint(pointers[0].position);

        if (Mathf.Abs(ship.position.x - pointerWorldPos.x) < 0.2f)
            return 0f;
        else
            return Mathf.Clamp((ship.position.x - pointerWorldPos.x) / maxOffsetX, -1f, 1f);
    }
}
