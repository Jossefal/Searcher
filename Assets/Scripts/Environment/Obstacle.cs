using UnityEngine;

#pragma warning disable 649

public class Obstacle : StatsController, IObstacle
{
    [SerializeField] private int damage;
    [SerializeField] private bool deathAfterDealingDamage;
    [SerializeField] private bool destroyAfterDeath;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        IStats targetStats = coll.gameObject.GetComponent<IStats>();

        if (targetStats != null)
        {
            targetStats.ReceiveDamage(damage);

            if (deathAfterDealingDamage)
                Death();
        }
    }

    protected void OnEnable()
    {
        RestoreHP();
    }

    protected override void Death()
    {
        base.Death();

        if (destroyAfterDeath)
            Destroy(gameObject);
    }

    public void Kill()
    {
        Death();
    }
}