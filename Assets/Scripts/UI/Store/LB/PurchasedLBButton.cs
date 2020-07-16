using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 649

public class PurchasedLBButton : LBButton
{
    [SerializeField] private ValueVariant currency;
    [SerializeField] private int price;
    [SerializeField] private Text priceText;

    protected override void Initialize()
    {
        if(DataManager.isLocalTestMode)
            price = 0;

        if (priceText != null)
            priceText.text = Converter.ConvertToString(price);

        CheckPurchasePower();
        DataManager.onDataChanged += CheckPurchasePower;
    }

    protected void CheckPurchasePower()
    {
        button.interactable = DataManager.CheckValueCount(currency, price);
    }

    protected override void OnOpen()
    {
        switch (currency)
        {
            case ValueVariant.DiamondsCount:
                DataManager.diamondsCount -= price;
                break;
            case ValueVariant.LivesCount:
                DataManager.livesCount -= price;
                break;
            case ValueVariant.Record:
                DataManager.record -= price;
                break;
        }
    }

    protected void OnDestroy()
    {
        DataManager.onDataChanged -= CheckPurchasePower;
    }
}
