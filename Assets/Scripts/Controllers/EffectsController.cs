using UnityEngine;

public class EffectsController : MonoBehaviour
{
    public GameObject deathEffect;

    public void SpawnDeathEffect()
    {
        Instantiate(deathEffect, transform.position, transform.rotation);
    }
}
