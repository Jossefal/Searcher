using UnityEngine;

#pragma warning disable 649

public class MovingObject : MonoBehaviour
{
    [HideInInspector] public new Transform transform;

    [SerializeField] private Vector3 direction;
    [SerializeField] private float speed;

    private void Awake()
    {
        transform = base.transform;
        direction.Normalize();
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
