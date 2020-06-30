using UnityEngine;

#pragma warning disable 649

public class ShipSkinSpawner : MonoBehaviour
{
    [SerializeField] private SkinsContainer skinsContainer;

    private void Awake()
    {
        Instantiate(skinsContainer.currentShipSkin.sprite, transform.position, Quaternion.identity, transform.parent);
        Destroy(gameObject);
    }
}
