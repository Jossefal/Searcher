using UnityEngine;

#pragma warning disable 649

[RequireComponent(typeof(RandomMover))]
public class AngleMover : MonoBehaviour
{
    private new Transform transform;
    private RandomMover mover;
    private Vector3 currentAngle;

    [SerializeField] private float maxAngle;
    [SerializeField] [Range(0f, 1f)] private float lerpSpeed = 0.1f;

    private void Start()
    {
        mover = GetComponent<RandomMover>();
        transform = GetComponent<Transform>();

        currentAngle = transform.eulerAngles;
    }

    private void Update()
    {
        float targetAngle = 0f;

        if (mover.distance > 0.1f)
            targetAngle = transform.localPosition.x > mover.destination.x ? maxAngle : -maxAngle;

        currentAngle.z = Mathf.Lerp(currentAngle.z, targetAngle, lerpSpeed);
        transform.eulerAngles = currentAngle;
    }
}
