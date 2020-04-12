using UnityEngine;
using UnityEngine.Events;

#pragma warning disable 649

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] private UTriggerEvent onTriggerEnter;
    [SerializeField] private UTriggerEvent onTriggerExit;

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
