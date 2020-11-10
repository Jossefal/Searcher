﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#pragma warning disable 649

public class MultiThemePanel : MonoBehaviour, IThemePanel
{
    [SerializeField] private MultiThemeData themeData;
    [SerializeField] private ThemesMenu childThemesMenu;
    [SerializeField] protected ThemesMenu themesMenu;
    [SerializeField] protected Button buyBtn;
    [SerializeField] protected Button selectBtn;
    [SerializeField] private Button changeBtn;
    [SerializeField] protected GameObject selectedIndicator;
    [SerializeField] protected GameObject shopOpener;

    private void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
        SetState();
        childThemesMenu.gameObject.SetActive(false);
    }

    public void Initialize()
    {
        selectBtn.onClick.AddListener(new UnityAction(() => childThemesMenu.gameObject.SetActive(true)));
        buyBtn?.onClick.AddListener(new UnityAction(BuyTheme));
        changeBtn.onClick.AddListener(new UnityAction(() => childThemesMenu.gameObject.SetActive(true)));

        childThemesMenu.onThemeChanged += SetState;
    }

    public void SetState()
    {
        if (CheckCurrentTheme())
            SetSelectedState();
        else if (themeData.Price == 0 || DataManager.themesIds.Contains(themeData.Id))
            SetUnlockedState();
        else
            SetLockedState();
    }

    private bool CheckCurrentTheme()
    {
        int currentThemeId = DataManager.currentThemeId.GetValue();

        for (int i = 0; i < themeData.ThemesCount; i++)
        {
            if (currentThemeId == themeData.GetChildTheme(i).Id)
                return true;
        }

        return false;
    }

    public void BuyTheme()
    {
        if (DataManager.CheckValueCount(ValueVariant.DiamondsCount, themeData.Price))
        {
            DataManager.diamondsCount -= themeData.Price;

            DataManager.themesIds.Add(themeData.Id);

            for (int i = 0; i < themeData.ThemesCount; i++)
            {
                DataManager.themesIds.Add(themeData.GetChildTheme(i).Id);
            }

            SetUnlockedState();
        }
        else
            shopOpener.SetActive(true);

        DataManager.LocalSave();
    }

    // public void SetCurrentSkin()
    // {
    //     DataManager.currentThemeId = new SafeInt(themeData.GetChildTheme(0).Id);
    //     Debug.Log(themeData.GetChildTheme(0).Id + "==" + DataManager.currentThemeId);

    //     SetSelectedState();
    // }

    public void SetLockedState()
    {
        selectBtn.gameObject.SetActive(false);
        selectedIndicator.SetActive(false);

        if (buyBtn != null)
            buyBtn.gameObject.SetActive(true);

        changeBtn.gameObject.SetActive(false);
    }

    public void SetUnlockedState()
    {
        selectBtn.gameObject.SetActive(true);
        selectedIndicator.SetActive(false);

        if (buyBtn != null)
            buyBtn.gameObject.SetActive(false);

        changeBtn.gameObject.SetActive(false);
    }

    public void SetSelectedState()
    {
        selectBtn.gameObject.SetActive(false);
        selectedIndicator.SetActive(true);

        if (buyBtn != null)
            buyBtn.gameObject.SetActive(false);

        changeBtn.gameObject.SetActive(true);

        themesMenu.SetSelectedPanel(gameObject);
    }
}
