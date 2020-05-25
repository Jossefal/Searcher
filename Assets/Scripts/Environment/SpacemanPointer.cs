using UnityEngine;

#pragma warning disable 649

public class SpacemanPointer : MonoBehaviour
{
    [HideInInspector] public new Transform transform;

    [SerializeField] private GameObject sprite;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Vector2 range;
    [SerializeField] private Transform shipTransform;
    [SerializeField] private AreasManager areasManager;

    private Vector3 pos = new Vector3(0f, 0f, 0f);

    private void Awake()
    {
        transform = base.transform;
    }

    private void Update()
    {
        Transform nearestSpaceman = areasManager.GetNearestSpaceman(cameraTransform.position);

        if (nearestSpaceman == null)
        {
            sprite.SetActive(false);
            return;
        }
        else
            sprite.SetActive(true);

        pos.x = Mathf.Clamp(nearestSpaceman.position.x, cameraTransform.position.x - range.x, cameraTransform.position.x + range.x);
        pos.y = Mathf.Clamp(nearestSpaceman.position.y, cameraTransform.position.y - range.y, cameraTransform.position.y + range.y);

        Vector3 up = (nearestSpaceman.transform.position - shipTransform.position).normalized;

        transform.position = pos;
        transform.up = up;
    }
}
