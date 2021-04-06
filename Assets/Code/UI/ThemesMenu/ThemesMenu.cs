using System;
using UnityEngine;

#pragma warning disable 649

public class ThemesMenu : MonoBehaviour
{
    public event Action onThemeChanged;

    [SerializeField] private SnapScroll snapScroll;

    private GameObject selectedThemePanel;

    // private void OnEnable()
    // {
    //     if (selectedThemePanel != null)
    //         snapScroll?.SetHighlightedObject(selectedThemePanel.transform);
    // }

    public void SetSelectedPanel(GameObject newSelectedPanel)
    {
        if (selectedThemePanel != null && selectedThemePanel != newSelectedPanel)
            selectedThemePanel.GetComponent<IThemePanel>().SetUnlockedState();

        snapScroll?.SetHighlightedObject(newSelectedPanel.transform);

        selectedThemePanel = newSelectedPanel;
    
        onThemeChanged?.Invoke();
    }
}
