using UnityEngine;

public class ShipSpanwer : MonoBehaviour
{
    public SkinsContainer skinsContainer;

    private void Awake()
    {
        Instantiate(skinsContainer.GetCurrentSkin(), transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
