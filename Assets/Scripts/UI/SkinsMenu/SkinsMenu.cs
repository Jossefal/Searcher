using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#pragma warning disable 649

public class SkinsMenu : MonoBehaviour
{
    [SerializeField] private SkinPanel[] skinPanels;
    [SerializeField] private SnapScroll snapScroll;

    private SkinPanel selectedSkinPanel;

    private void Awake()
    {
        if (skinPanels.Length == 0)
            return;

        for (int i = 0; i < skinPanels.Length; i++)
        {
            skinPanels[i].onSelect = SetSelectedPanel;
            skinPanels[i].Initialize();
        }
    }

    private void OnEnable()
    {
        snapScroll.SetHighlightedObject(selectedSkinPanel.panelTransform);
    }

    private void SetSelectedPanel(SkinPanel newSelectedPanel)
    {
        if (selectedSkinPanel != null)
            selectedSkinPanel.Deselect();

        selectedSkinPanel = newSelectedPanel;
        snapScroll.SetHighlightedObject(selectedSkinPanel.panelTransform);
    }
}

[System.Serializable]
public class SkinPanel
{
    public SkinData skinData;

    public enum SkinType
    {
        Ship,
        Environment
    }

    public SkinType skinType;
    public Transform panelTransform;
    public Button selectBtn;
    public GameObject selectedIndicator;
    public Action<SkinPanel> onSelect;

    public void Initialize()
    {
        selectBtn.onClick.AddListener(new UnityAction(SetCurrentSkin));

        if (skinType == SkinType.Ship)
        {
            if (DataManager.currentShipSkinId.GetValue() == skinData.id)
                Select();
            else
                Deselect();
        }
        else
        {
            if (DataManager.currentEnvironmentSkinId.GetValue() == skinData.id)
                Select();
            else
                Deselect();
        }
    }

    public void SetCurrentSkin()
    {
        if (skinType == SkinType.Ship)
            SkinManager.SetCurrentShipSkin(skinData.id);
        else
            SkinManager.SetCurrentEnvironemntSkin(skinData.id);

        Select();
    }

    public void Select()
    {
        selectBtn.gameObject.SetActive(false);
        selectedIndicator.SetActive(true);

        if (onSelect != null)
            onSelect(this);
    }

    public void Deselect()
    {
        selectBtn.gameObject.SetActive(true);
        selectedIndicator.SetActive(false);
    }
}
