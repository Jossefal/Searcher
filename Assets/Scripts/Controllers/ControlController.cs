using UnityEngine;
using UnityEngine.EventSystems;

#pragma warning disable 649

public class ControlController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float maxOffsetX = 1f;

    [SerializeField] private Transform ship;
    [SerializeField] private Camera mainCamera;

    private Vector2 startPos;
    private PointerEventData pointer;

    public void OnPointerDown(PointerEventData ped)
    {
        if (pointer == null)
        {
            pointer = ped;
            startPos = ped.position;
        }
    }

    public void OnPointerUp(PointerEventData ped)
    {
        if (pointer == ped)
            pointer = null;
    }

    public float Horizontal()
    {
        if (pointer == null)
            return 0f;

        Vector2 pointerWorldPos = mainCamera.ScreenToWorldPoint(pointer.position);

        if (Mathf.Abs(ship.position.x - pointerWorldPos.x) < 0.2f)
            return 0f;
        else
            return Mathf.Clamp((ship.position.x - pointerWorldPos.x) / maxOffsetX, -1f, 1f);
    }
}
