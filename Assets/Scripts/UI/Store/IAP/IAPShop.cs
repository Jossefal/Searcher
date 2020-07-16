using UnityEngine;
using UnityEngine.Purchasing;

#pragma warning disable 649

public class IAPShop : MonoBehaviour
{
    [SerializeField] private PurchasesContainer purchasesContainer;
    [SerializeField] private GameObject initializationFailedIndicator;

    private void Awake()
    {
        if (!IAPManager.isInitialized)
        {
            if (initializationFailedIndicator != null)
                initializationFailedIndicator.SetActive(true);

            return;
        }

        IAPManager.Instance.onSuccessConsumable += ProccesConsumable;
    }

    private void ProccesConsumable(Product product)
    {
        if (purchasesContainer.Contains(product.definition.id))
        {
            PurchaseData purchase = purchasesContainer[product.definition.id];

            switch (purchase.type)
            {
                case PurchaseType.Lives:
                    {
                        DataManager.livesCount += ((CurrencyPurchaseData)purchase).count;
                    }
                    break;
                case PurchaseType.Diamonds:
                    {
                        DataManager.diamondsCount += ((CurrencyPurchaseData)purchase).count;
                    }
                    break;
            }
        }

        DataManager.LocalAndCloudSave((status) =>
        {
            if (status)
                IAPManager.Instance.ConfirmPendingPurchase(product);
        });
    }

    private void OnDestroy()
    {
        if (IAPManager.isInitialized)
            IAPManager.Instance.onSuccessConsumable -= ProccesConsumable;
    }
}