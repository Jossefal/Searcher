using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#pragma warning disable 649

public class SkinPanel : MonoBehaviour
{
    [SerializeField] private SkinsMenu skinsMenu;
    [SerializeField] private SkinData skinData;

    public enum SkinType
    {
        Ship,
        Environment
    }

    [SerializeField] private SkinType skinType;
    [SerializeField] private Text priceText;
    [SerializeField] private Button buyBtn;
    [SerializeField] private Button selectBtn;
    [SerializeField] private GameObject selectedIndicator;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        selectBtn.onClick.AddListener(new UnityAction(SetCurrentSkin));
        buyBtn.onClick.AddListener(new UnityAction(BuySkin));

        if (skinType == SkinType.Ship)
        {
            if (!DataManager.shipSkinIds.Contains(skinData.id))
                SetLockedState();
            else if (DataManager.currentShipSkinId.GetValue() != skinData.id)
                SetUnlockedState();
            else
                SetSelectedState();
        }
        else if (skinType == SkinType.Environment)
        {
            if (!DataManager.environmentSkinIds.Contains(skinData.id))
                SetLockedState();
            else if (DataManager.currentEnvironmentSkinId.GetValue() != skinData.id)
                SetUnlockedState();
            else
                SetSelectedState();
        }
    }

    public void BuySkin()
    {
        bool result;
        if (skinType == SkinType.Ship)
            result = SkinManager.BuyShipSkin(skinData);
        else
            result = SkinManager.BuyEnvironemntSkin(skinData);

        if (result)
            SetCurrentSkin();
    }

    public void SetCurrentSkin()
    {
        if (skinType == SkinType.Ship)
            SkinManager.SetCurrentShipSkin(skinData.id);
        else
            SkinManager.SetCurrentEnvironemntSkin(skinData.id);

        SetSelectedState();
    }

    public void SetLockedState()
    {
        selectBtn.gameObject.SetActive(false);
        selectedIndicator.SetActive(false);
        priceText.text = Converter.ConvertToString(skinData.price);
        priceText.gameObject.SetActive(true);
        buyBtn.gameObject.SetActive(true);
    }

    public void SetUnlockedState()
    {
        selectBtn.gameObject.SetActive(true);
        selectedIndicator.SetActive(false);
        priceText.gameObject.SetActive(false);
        buyBtn.gameObject.SetActive(false);
    }

    public void SetSelectedState()
    {
        selectBtn.gameObject.SetActive(false);
        selectedIndicator.SetActive(true);
        priceText.gameObject.SetActive(false);
        buyBtn.gameObject.SetActive(false);
        skinsMenu.SetSelectedPanel(this);
    }
}
