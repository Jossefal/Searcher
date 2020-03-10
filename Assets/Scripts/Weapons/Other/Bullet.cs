using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float force;
    public float damage;
    public float lifeTime;

    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * force, ForceMode2D.Impulse);
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Asteroid")
            coll.gameObject.GetComponent<StatsController>().ReceiveDamage(damage);
            
        Destroy(gameObject);
    }
}
