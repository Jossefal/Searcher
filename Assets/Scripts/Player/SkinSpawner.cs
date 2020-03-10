using UnityEngine;

public class SkinSpawner : MonoBehaviour
{
    public SkinsContainer skinsContainer;

    private void Awake()
    {
        Instantiate(skinsContainer.GetCurrentSkin(), transform.position, Quaternion.identity, transform.parent);
        Destroy(gameObject);
    }
}
