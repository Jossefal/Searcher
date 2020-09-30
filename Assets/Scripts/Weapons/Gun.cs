using System.Collections;
using UnityEngine;

#pragma warning disable 649

public class Gun : MonoBehaviour
{
    [HideInInspector] public new Transform transform;

    private float delayStartTime;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float startDelay;
    [SerializeField] private float shootDelay;
    [SerializeField] private float bulletLifetime;
    [SerializeField] private Transform shootPoint;

    public enum DirectionType
    {
        ShootPointUp,
        Up,
        Down
    }

    [SerializeField] private DirectionType directionType;

    private void Awake()
    {
        transform = GetComponent<Transform>();
    }

    private void OnEnable()
    {
        StartCoroutine(Fire());
    }

    private IEnumerator Fire()
    {
        yield return new WaitForSeconds(startDelay);

        delayStartTime = Time.time - shootDelay;

        while (true)
        {
            if (Time.time > delayStartTime + shootDelay)
            {
                GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

                switch (directionType)
                {
                    case DirectionType.Up:
                        bullet.transform.up = Vector3.up;
                        break;
                    case DirectionType.Down:
                        bullet.transform.up = Vector3.down;
                        break;
                }

                Destroy(bullet, bulletLifetime);

                delayStartTime = Time.time;
            }

            yield return null;
        }
    }
}
