using UnityEngine;

public class RandomDirectionGun : Gun
{
    [SerializeField] private float angleVariation;

    protected override void SetRotation(Transform bulletTransform)
    {
        base.SetRotation(bulletTransform);

        float angle = Random.Range(-angleVariation, angleVariation);
        bulletTransform.Rotate(new Vector3(0f, 0f, angle));
    }
}
