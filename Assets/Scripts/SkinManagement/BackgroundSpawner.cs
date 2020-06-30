using UnityEngine;

#pragma warning disable 649

public class BackgroundSpawner : MonoBehaviour
{
    [SerializeField] private SkinsContainer skinsContainer;

    void Awake()
    {
        Instantiate(skinsContainer.currentEnvironemntSkin.background, transform.position, Quaternion.identity, transform.parent);
        Destroy(gameObject);
    }
}
