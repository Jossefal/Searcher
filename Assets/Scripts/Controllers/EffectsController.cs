using UnityEngine;

#pragma warning disable 649

public class EffectsController : MonoBehaviour
{
    public GameObject deathEffectPrefab { get; set; }

    public void SpawnDeathEffect()
    {
        if (deathEffectPrefab != null)
            Instantiate(deathEffectPrefab, transform.position, transform.rotation);
    }
}