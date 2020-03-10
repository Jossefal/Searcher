using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PointerEvents : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] UnityEvent OnPointerDownEvent = null;

    public void OnPointerDown(PointerEventData ped)
    {
        OnPointerDownEvent.Invoke();
    }
}
