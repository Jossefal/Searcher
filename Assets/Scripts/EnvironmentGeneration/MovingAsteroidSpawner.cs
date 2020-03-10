using UnityEngine;

public class MovingAsteroidSpawner : MonoBehaviour
{
    public GameObject movingAsteroidPrefab;
    public Transform leftPoint;
    public Transform rightPoint;

    private void Start()
    {
        int choice = Random.Range(0, 10);
        MovingObject movingAsteroid = Instantiate(movingAsteroidPrefab, choice % 2 != 0 ? leftPoint.position : rightPoint.position, Quaternion.identity, transform).GetComponent<MovingObject>();
        movingAsteroid.customDirection = choice % 2 != 0 ? Vector3.right : Vector3.left;
    }
}
