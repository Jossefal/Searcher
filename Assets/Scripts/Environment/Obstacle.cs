using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float damage;
    public GameObject deathEffect;
    public bool deathAfterDealingDamage;

    protected virtual void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Ship")
        {
            coll.gameObject.GetComponent<StatsController>().ReceiveDamage(damage);

            if (deathAfterDealingDamage)
                Death();
        }
    }

    public void Death()
    {
        Instantiate(deathEffect, transform.position, deathEffect.transform.rotation);
        gameObject.SetActive(false);
    }
}