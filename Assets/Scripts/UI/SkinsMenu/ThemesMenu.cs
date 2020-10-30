using UnityEngine;

#pragma warning disable 649

public class ThemesMenu : MonoBehaviour
{
    [SerializeField] private SnapScroll snapScroll;

    private GameObject selectedThemePanel;

    private void OnEnable()
    {
        if (selectedThemePanel != null)
            snapScroll?.SetHighlightedObject(selectedThemePanel.transform);
    }

    public void SetSelectedPanel(GameObject newSelectedPanel)
    {
        if (selectedThemePanel != null && selectedThemePanel != newSelectedPanel)
            selectedThemePanel.GetComponent<IThemePanel>().SetUnlockedState();

        selectedThemePanel = newSelectedPanel;
    }
}
