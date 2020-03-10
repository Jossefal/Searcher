using UnityEngine;
using UnityEngine.EventSystems;

public class TouchTest : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private PointerEventData ped;

    public void OnPointerDown(PointerEventData ped)
    {
        this.ped = ped;
        Debug.Log("ENTER-" + ped.pointerId);
    }

    public void OnPointerUp(PointerEventData ped)
    {
        if(this.ped == ped)
            Debug.Log("EXIT-" + ped.pointerId);
    }
}
