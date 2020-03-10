using UnityEngine;
using UnityEngine.EventSystems;

public class ControlController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public InterfaceManager interfaceManager;
    public float maxOffsetX = 140f;
    public Transform ship;
    public ShipStats shipStats;

    public enum ControlType
    {
        Type1,
        Type2
    }

    public ControlType controlType = ControlType.Type1;

    private Vector2 startPos;

    private PointerEventData pointer;

    public void OnPointerDown(PointerEventData ped)
    {
        if(pointer == null)
        {
            pointer = ped;
            startPos = ped.position;
        }
    }

    public void OnPointerUp(PointerEventData ped)
    {
        if(pointer == ped)
            pointer = null;
    }

    public virtual float Horizontal()
    {
        if(pointer == null)
            return 0f;

        switch (controlType)
        {
            case ControlType.Type1:
                return Mathf.Clamp((startPos.x - pointer.position.x) / maxOffsetX, -1f, 1f);
            case ControlType.Type2:
                {
                    Vector2 pointerWorldPos = Camera.main.ScreenToWorldPoint(pointer.position);
                    if (Mathf.Abs(ship.position.x - pointerWorldPos.x) < 0.2f)
                        return 0f;
                    else
                        return Mathf.Clamp((ship.position.x - pointerWorldPos.x) / maxOffsetX, -1f, 1f);
                }
            default:
                return 0f;
        }
    }
}
