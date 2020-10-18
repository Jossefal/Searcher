using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#pragma warning disable 649

public class ThemePanel : MonoBehaviour
{
    [SerializeField] private ThemesMenu themesMenu;
    [SerializeField] private int themeId;
    [SerializeField] private int price;
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

        if (DataManager.currentThemeId.GetValue() == themeId)
            SetSelectedState();
        else if (DataManager.themesIds.Contains(themeId))
            SetUnlockedState();
        else
            SetLockedState();
    }

    public void BuySkin()
    {
        if (DataManager.diamondsCount.GetValue() >= price)
        {
            DataManager.diamondsCount -= price;
            DataManager.themesIds.Add(themeId);
            SetCurrentSkin();
        }
    }

    public void SetCurrentSkin()
    {
        DataManager.currentThemeId = new SafeInt(themeId);

        SetSelectedState();
    }

    public void SetLockedState()
    {
        selectBtn.gameObject.SetActive(false);
        selectedIndicator.SetActive(false);
        priceText.text = Converter.ConvertToString(price);
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
        themesMenu.SetSelectedPanel(this);
    }
}
