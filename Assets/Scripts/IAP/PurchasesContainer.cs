using UnityEngine;

#pragma warning disable 649

[CreateAssetMenu(fileName = "NewPurchasesContainer", menuName = "ScriptableObject`s/PurchasesContainer")]
public class PurchasesContainer : ScriptableObject
{
    [SerializeField] private PurchaseData[] purchases;

    public PurchaseData this[string id]
    {
        get
        {
            for (int i = 0; i < purchases.Length; i++)
            {
                if (purchases[i].id == id)
                    return purchases[i];
            }

            return null;
        }
    }

    public bool Contains(string id)
    {
        for (int i = 0; i < purchases.Length; i++)
        {
            if (purchases[i].id == id)
                return true;
        }

        return false;
    }
}

public enum PurchaseType
{
    Lives,
    Diamonds
}
