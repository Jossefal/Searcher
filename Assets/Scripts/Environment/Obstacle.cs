using UnityEngine;

#pragma warning disable 649

public class Obstacle : MonoBehaviour, IObstacle
{
    private EffectsController effectsController;

    [SerializeField] private float damage;
    [SerializeField] private bool deathAfterDealingDamage;

    private void Start()
    {
        effectsController = GetComponent<EffectsController>();
    }

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
        effectsController.SpawnDeathEffect();
        gameObject.SetActive(false);
    }

    public void Kill()
    {
        Death();
    }
}