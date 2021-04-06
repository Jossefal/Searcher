using UnityEngine;

#pragma warning disable 649

public abstract class PurchaseData : ScriptableObject
{
    [SerializeField] protected PurchaseType _type;
    [SerializeField] protected string _id;

    public PurchaseType type
    {
        get => _type;
    }

    public string id
    {
        get => _id;
    }
}
