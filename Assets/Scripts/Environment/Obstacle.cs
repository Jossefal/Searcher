using UnityEngine;

#pragma warning disable 649

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private bool deathAfterDealingDamage;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Ship")
        {
            coll.gameObject.GetComponent<StatsController>().ReceiveDamage(damage);

            if (deathAfterDealingDamage)
                Death();
        }
    }

    private void Death()
    {
        Instantiate(deathEffect, transform.position, deathEffect.transform.rotation);
        gameObject.SetActive(false);
    }

    public void Kill()
    {
        Death();
    }
}