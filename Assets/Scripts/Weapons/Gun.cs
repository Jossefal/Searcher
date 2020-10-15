using System.Collections;
using UnityEngine;

#pragma warning disable 649

public class Gun : MonoBehaviour
{
    [HideInInspector] public new Transform transform;

    protected float delayStartTime;

    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected float startDelay;
    [SerializeField] protected float shootDelay;
    [SerializeField] protected float bulletLifetime;
    [SerializeField] protected Transform shootPoint;

    public enum DirectionType
    {
        ShootPointUp,
        Up,
        Down
    }

    [SerializeField] protected DirectionType directionType;

    protected void Awake()
    {
        transform = GetComponent<Transform>();
    }

    protected void OnEnable()
    {
        StartCoroutine(Fire());
    }

    protected IEnumerator Fire()
    {
        yield return new WaitForSeconds(startDelay);

        delayStartTime = Time.time - shootDelay;

        while (true)
        {
            if (Time.time > delayStartTime + shootDelay)
            {
                GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

                SetRotation(bullet.transform);

                Destroy(bullet, bulletLifetime);

                delayStartTime = Time.time;
            }

            yield return null;
        }
    }

    protected virtual void SetRotation(Transform bulletTransform)
    {
        switch (directionType)
        {
            case DirectionType.ShootPointUp:
                bulletTransform.up = shootPoint.up;
                break;
            case DirectionType.Up:
                bulletTransform.up = Vector3.up;
                break;
            case DirectionType.Down:
                bulletTransform.up = Vector3.down;
                break;
            default:
                bulletTransform.up = Vector3.up;
                break;
        }
    }
}
