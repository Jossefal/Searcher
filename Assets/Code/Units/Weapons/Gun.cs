using System.Collections;
using UnityEngine;

#pragma warning disable 649

public class Gun : MonoBehaviour
{
    [HideInInspector] public new Transform transform;

    protected float delayStartTime;

    [SerializeField] protected GameObject bulletPrefab;
    public GameObject BulletPrefab
    {
        get
        {
            return bulletPrefab;
        }

        set
        {
            bulletPrefab = value;
        }
    }

    [SerializeField] protected float startDelay;
    [SerializeField] protected float shootDelay;
    [SerializeField] protected float bulletLifetime;
    [SerializeField] protected Transform shootPoint;
    [SerializeField] protected Transform bulletParent;

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
                GameObject bullet = null;

                if (bulletParent == null)
                    bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
                else
                    bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity, bulletParent);

                SetRotation(bullet.transform);

                Destroy(bullet, bulletLifetime);

                delayStartTime = Time.time;
            }

            yield return null;
        }
    }

    protected virtual void SetRotation(Transform bullet)
    {
        switch (directionType)
        {
            case DirectionType.ShootPointUp:
                bullet.up = shootPoint.up;
                break;
            case DirectionType.Up:
                bullet.up = Vector3.up;
                break;
            case DirectionType.Down:
                bullet.up = Vector3.down;
                break;
            default:
                bullet.up = Vector3.up;
                break;
        }
    }
}
