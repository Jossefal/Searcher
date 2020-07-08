using UnityEngine;

#pragma warning disable 649

public class SkinsMenu : MonoBehaviour
{
    [SerializeField] private SkinPanel[] skinPanels;
    [SerializeField] private SnapScroll snapScroll;

    private SkinPanel selectedSkinPanel;

    private void OnEnable()
    {
        if (selectedSkinPanel != null)
            snapScroll.SetHighlightedObject(selectedSkinPanel.transform);
    }

    public void SetSelectedPanel(SkinPanel newSelectedPanel)
    {
        if (selectedSkinPanel != null)
            selectedSkinPanel.SetUnlockedState();

        selectedSkinPanel = newSelectedPanel;
        snapScroll.SetHighlightedObject(selectedSkinPanel.transform);
    }
}
