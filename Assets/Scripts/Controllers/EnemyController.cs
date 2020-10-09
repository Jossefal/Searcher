using UnityEngine;

#pragma warning disable 649

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    public void SetLifetime(float time)
    {
        Invoke("Kill", time);
    }

    public void Kill()
    {
        if (enemy.activeInHierarchy)
            enemy?.GetComponent<IObstacle>().Kill();
    }
}
