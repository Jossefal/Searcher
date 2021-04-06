using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#pragma warning disable 649

public class ThemePanel : MonoBehaviour, IThemePanel
{
    [SerializeField] protected ThemesMenu themesMenu;
    [SerializeField] protected ThemeData themeData;
    [SerializeField] protected Button buyBtn;
    [SerializeField] protected Button selectBtn;
    [SerializeField] protected GameObject selectedIndicator;
    [SerializeField] protected GameObject shopOpener;

    private void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
        SetState();
    }

    public void Initialize()
    {
        selectBtn.onClick.AddListener(new UnityAction(SetCurrentSkin));
        buyBtn?.onClick.AddListener(new UnityAction(BuyTheme));
    }

    public void SetState()
    {
        if (DataManager.currentThemeId.GetValue() == themeData.Id)
            SetSelectedState();
        else if (themeData.Price == 0 || DataManager.themesIds.Contains(themeData.Id))
            SetUnlockedState();
        else
            SetLockedState();
    }

    public void BuyTheme()
    {
        if (DataManager.CheckValueCount(ValueVariant.DiamondsCount, themeData.Price))
        {
            DataManager.diamondsCount -= themeData.Price;
            DataManager.themesIds.Add(themeData.Id);
            SetCurrentSkin();
        }
        else
            shopOpener.SetActive(true);
        
        DataManager.LocalSave();
    }

    public void SetCurrentSkin()
    {
        DataManager.currentThemeId = new SafeInt(themeData.Id);

        SetSelectedState();
    }

    public void SetLockedState()
    {
        selectBtn.gameObject.SetActive(false);
        selectedIndicator.SetActive(false);

        if (buyBtn != null)
            buyBtn.gameObject.SetActive(true);
    }

    public void SetUnlockedState()
    {
        selectBtn.gameObject.SetActive(true);
        selectedIndicator.SetActive(false);

        if (buyBtn != null)
            buyBtn.gameObject.SetActive(false);
    }

    public void SetSelectedState()
    {
        selectBtn.gameObject.SetActive(false);
        selectedIndicator.SetActive(true);

        if (buyBtn != null)
            buyBtn.gameObject.SetActive(false);

        themesMenu.SetSelectedPanel(gameObject);
    }
}
