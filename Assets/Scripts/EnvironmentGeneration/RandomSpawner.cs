using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] spawnObjects;
    public bool withChances;
    public float[] chances;
    public float xRadius;
    public float yRadius;

    private void Awake()
    {
        if(withChances)
            Sort();
    }

    private void Start()
    {
        int index = withChances ? ChooseIndex() : Random.Range(0, spawnObjects.Length);
        Instantiate(spawnObjects[index], new Vector3(Random.Range(transform.position.x - xRadius, transform.position.x + xRadius), Random.Range(transform.position.y - yRadius, transform.position.y + yRadius), 0), transform.rotation, transform);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(xRadius * 2, yRadius * 2, 0));
    }
    
    private int ChooseIndex()
    {
        if(spawnObjects.Length == 1)
            return 0;
        float total = 0;
        foreach(float elem in chances){
            total += elem;
        }
        float randomValue = Random.value * total;
        for(int i= 0; i < chances.Length; i++){
            if(randomValue < chances[i]){
                return i;
            }
            else{
                randomValue -= chances[i];
            }
        }
        return chances.Length - 1;
    }

    private void Sort()
    {
        for(int i = 0; i < chances.Length - 1; i++)
        {
            for(int j = i + 1; j < chances.Length; j++)
            {
                if(chances[i] < chances[j])
                {
                    float tmpChance = chances[i];
                    GameObject tmpObject = spawnObjects[i];
                    chances[i] = chances[j];
                    spawnObjects[i] = spawnObjects[j];
                    chances[j] = tmpChance;
                    spawnObjects[j] = tmpObject;
                }
            }
        }
    }
}
