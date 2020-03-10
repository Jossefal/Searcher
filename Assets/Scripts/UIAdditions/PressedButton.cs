using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PressedButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isPressed = false;

    [SerializeField] private UnityEvent onPressed = null;

    private void Update()
    {
        if (isPressed)
            onPressed.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }
}
