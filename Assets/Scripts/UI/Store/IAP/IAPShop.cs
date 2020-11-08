using UnityEngine;
using UnityEngine.Purchasing;

#pragma warning disable 649

public class IAPShop : MonoBehaviour
{
    [SerializeField] private PurchasesContainer purchasesContainer;
    [SerializeField] private GameObject initializationFailedIndicator;

    [System.Serializable]
    public class PurchaseResultPanel
    {
        public string purchase_id;
        public GameObject gameObject;
    }

    [SerializeField] private PurchaseResultPanel[] resultPanels;

    private void Awake()
    {
        if (!IAPManager.isInitialized || !GPGSManager.isAuthenticated)
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

        for (int i = 0; i < resultPanels.Length; i++)
        {
            if (resultPanels[i].purchase_id == product.definition.id)
            {
                resultPanels[i].gameObject.SetActive(true);
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