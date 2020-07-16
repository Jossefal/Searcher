using System;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : IStoreListener
{
    public static IAPManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public static bool isInitialized
    {
        get
        {
            return _instance != null && _instance.controller != null && _instance.extensions != null;
        }
    }

    public static bool isInitialising { get; private set; }

    private static IAPManager _instance;
    private static Action<bool> initializationCallback;
    private static ProductDefinition[] productDefinitions = new ProductDefinition[]
    {
        new ProductDefinition("lives_25", ProductType.Consumable)
    };

    public event Action<Product> onSuccessConsumable;
    public event Action<Product> onSuccessNonConsumable;
    public event Action<Product> onPurchaseFailed;

    private IStoreController controller;
    private IExtensionProvider extensions;

    public static void Initialize(Action<bool> callback)
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            callback?.Invoke(false);
        }

        initializationCallback = callback;
        InitializeInstance();
    }

    private static void InitializeInstance()
    {
        _instance = new IAPManager();
    }

    private IAPManager()
    {
        isInitialising = true;

        ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProducts(productDefinitions);

        UnityPurchasing.Initialize(this, builder);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        isInitialising = false;

        this.controller = controller;
        this.extensions = extensions;

        initializationCallback?.Invoke(true);
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        isInitialising = false;
        initializationCallback?.Invoke(false);
    }

    public void InitiatePurchase(Product product)
    {
        if (isInitialized && product != null && product.availableToPurchase)
            controller.InitiatePurchase(product);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        if (e.purchasedProduct.definition.type == ProductType.Consumable)
        {
            onSuccessConsumable?.Invoke(e.purchasedProduct);

            return PurchaseProcessingResult.Pending;
        }
        else
        {
            onSuccessNonConsumable?.Invoke(e.purchasedProduct);

            return PurchaseProcessingResult.Complete;
        }
    }

    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        onPurchaseFailed?.Invoke(i);
    }

    public void ConfirmPendingPurchase(Product product)
    {
        controller.ConfirmPendingPurchase(product);
    }

    public Product GetProductMetadata(string id)
    {
        if (!isInitialized)
            return null;

        return controller.products.WithID(id);
    }
}
