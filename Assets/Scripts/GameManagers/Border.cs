using UnityEngine;
using UnityEngine.Events;

public class Border : MonoBehaviour
{
    [SerializeField] private BorderEvent borderEvent = null;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        borderEvent.Invoke(coll.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        borderEvent.Invoke(coll.gameObject);
    }
}

[System.Serializable]
public class BorderEvent : UnityEvent<GameObject>
{
    //TODO
}