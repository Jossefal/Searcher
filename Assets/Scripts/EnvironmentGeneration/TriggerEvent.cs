using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] private UTriggerEvent onTriggerEnter = null;
    [SerializeField] private UTriggerEvent onTriggerExit = null;

    private void OnTriggerEnter2D(Collider2D col)
    {
        onTriggerEnter.Invoke(col.gameObject);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        onTriggerExit.Invoke(col.gameObject);
    }
}

[System.Serializable]
public class UTriggerEvent : UnityEvent<GameObject>
{
    //TODO
}
