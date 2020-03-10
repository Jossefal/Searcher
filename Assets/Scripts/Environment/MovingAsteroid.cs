using UnityEngine;

public class MovingAsteroid : Obstacle
{
    public float speedRndMin;
    public float speedRndMax;

    private MovingObject movingObject = null;

    private void Awake()
    {
        movingObject = GetComponent<MovingObject>();
        movingObject.speed = Random.Range(speedRndMin, speedRndMax);
    }
}
