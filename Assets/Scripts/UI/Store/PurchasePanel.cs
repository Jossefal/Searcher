using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;

#pragma warning disable 649

public class PurchasePanel : MonoBehaviour
{
    [SerializeField] private PurchaseData purchaseData;
    [SerializeField] private Text priceText;
    [SerializeField] private Button buyBtn;

    private Product product;

    private void Awake()
    {
        if (!IAPManager.isInitialized)
            return;

        product = IAPManager.Instance.GetProductMetadata(purchaseData.id);

        priceText.text = product.metadata.localizedPriceString;

        if (product.availableToPurchase)
            buyBtn.onClick.AddListener(() => IAPManager.Instance.InitiatePurchase(product));
        else
            buyBtn.interactable = false;
    }
}
