using UnityEngine;
using UnityEngine.EventSystems;

#pragma warning disable 649

public class ControlController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float maxOffsetX = 1f;

    private Vector2 startPos;
    private PointerEventData pointer;
    private float centerX;

    private void Start()
    {
        centerX = Screen.width / 2f;
    }

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
        
        float offset = centerX - pointer.position.x;

        if (Mathf.Abs(offset) < 10f)
            return 0f;
        else
            return Mathf.Clamp((offset) / maxOffsetX, -1f, 1f);
    }
}