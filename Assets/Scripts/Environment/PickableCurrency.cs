using UnityEngine;

#pragma warning disable 649

public class PickableCurrency : MonoBehaviour
{
    [SerializeField] private ValueVariant currencyVariant;
    [SerializeField] private uint count;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        switch (currencyVariant)
        {
            case ValueVariant.DiamondsCount:
                DataManager.diamondsCount += (int)count;
                break;
            case ValueVariant.LivesCount:
                DataManager.livesCount += (int)count;
                break;
        }

        gameObject.SetActive(false);
    }
}
