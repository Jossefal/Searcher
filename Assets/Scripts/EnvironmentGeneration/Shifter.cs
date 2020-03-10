using UnityEngine;

public class Shifter : MonoBehaviour
{
    public Vector3 rndRadius;

    private void Start()
    {
        transform.position += new Vector3(Random.Range(-rndRadius.x, rndRadius.x), Random.Range(-rndRadius.y, rndRadius.y), Random.Range(-rndRadius.z, rndRadius.z));
    }
}
