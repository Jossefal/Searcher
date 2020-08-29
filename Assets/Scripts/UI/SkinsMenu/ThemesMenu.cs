using UnityEngine;

#pragma warning disable 649

public class ThemesMenu : MonoBehaviour
{
    [SerializeField] private ThemePanel[] themePanels;
    [SerializeField] private SnapScroll snapScroll;

    private ThemePanel selectedThemePanel;

    private void OnEnable()
    {
        if (selectedThemePanel != null)
            snapScroll.SetHighlightedObject(selectedThemePanel.transform);
    }

    public void SetSelectedPanel(ThemePanel newSelectedPanel)
    {
        if (selectedThemePanel != null)
            selectedThemePanel.SetUnlockedState();

        selectedThemePanel = newSelectedPanel;
        snapScroll.SetHighlightedObject(selectedThemePanel.transform);
    }
}
