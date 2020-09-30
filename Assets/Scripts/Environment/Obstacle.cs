using UnityEngine;

#pragma warning disable 649

public class Obstacle : MonoBehaviour, IObstacle, IStats
{
    private EffectsController effectsController;
    private float currentHp;

    [SerializeField] private float hp;
    [SerializeField] private float damage;
    [SerializeField] private bool deathAfterDealingDamage;
    [SerializeField] private bool destroyAfterDeath;

    private void Start()
    {
        effectsController = GetComponent<EffectsController>();
    }

    private void OnEnable()
    {
        currentHp = hp;
    }

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

    public void ReceiveDamage(float damage)
    {
        hp = Mathf.Clamp(hp - damage, 0f, hp);

        if (hp == 0)
            Death();
    }

    private void Death()
    {
        if (effectsController != null)
            effectsController.SpawnDeathEffect();

        if (destroyAfterDeath)
            Destroy(gameObject);
        else
            gameObject.SetActive(false);
    }

    public void Kill()
    {
        Death();
    }
}