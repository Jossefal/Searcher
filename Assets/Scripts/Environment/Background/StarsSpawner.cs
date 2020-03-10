using UnityEngine;

public class StarsSpawner : MultipleSpawner
{
    public Color color;

    private void Awake()
    {
        color.r = Mathf.Clamp(PlayerPrefs.GetInt("R", 255), 0, 255);
        color.r = Mathf.Clamp(PlayerPrefs.GetInt("G", 255), 0, 255);
        color.r = Mathf.Clamp(PlayerPrefs.GetInt("B", 255), 0, 255);

        for (int i = 0; i < count; i++)
        {
            Instantiate(spawnObject, new Vector3(Random.Range(transform.position.x - xRadius, transform.position.x + xRadius), Random.Range(transform.position.y - yRadius, transform.position.y + yRadius), transform.position.z), transform.rotation, transform.parent);
            Destroy(gameObject);
        }
    }
}
