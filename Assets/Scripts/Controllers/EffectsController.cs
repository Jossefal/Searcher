using UnityEngine;

#pragma warning disable 649

public class EffectsController : MonoBehaviour
{
    [SerializeField] private GameObject deathEffect;

    public void SpawnDeathEffect()
    {
        Instantiate(deathEffect, transform.position, transform.rotation);
    }
}