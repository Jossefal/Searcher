using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public float fireRate;
    public float rotateSpeed;
    public Transform firePoint;
    public DetectionTrigger detectionTrigger;

    private float startShootTime;

    private void OnEnable()
    {
        startShootTime = Time.time - fireRate;
    }

    private void Update()
    {
        if (enabled == true)
        {
            GameObject nearestObject = detectionTrigger.GetNearestObject();
            Quaternion targetRotation;

            if (nearestObject != null)
                targetRotation = Quaternion.Euler(0, 0, Vector3.SignedAngle(detectionTrigger.transform.up, nearestObject.transform.position - detectionTrigger.transform.position, Vector3.forward));
            else
                targetRotation = Quaternion.Euler(0, 0, 0);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed);

            // if (nearestObject != null)
            //     transform.up = (nearestObject.transform.position - detectionTrigger.transform.position).normalized;
            // else
            //     transform.up = detectionTrigger.transform.up;

            if (Time.time > startShootTime + fireRate)
            {
                startShootTime = Time.time;
                Instantiate(bullet, firePoint.position, firePoint.rotation);
            }
        }
    }
}
