using UnityEngine;

#pragma warning disable 649

[CreateAssetMenu(fileName = "NewCurrencyPurchaseData", menuName = "ScriptableObject`s/CurrencyPurchaseData")]
public class CurrencyPurchaseData : PurchaseData
{
    [SerializeField] protected int _count;

    public int count
    {
        get => _count;
    }
}
