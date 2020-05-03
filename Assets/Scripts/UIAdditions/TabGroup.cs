using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 649

public class TabGroup : MonoBehaviour
{
    [SerializeField] private Button[] tabButtons;
    [SerializeField] private GameObject[] tabObjects;
    [SerializeField] private int activeTabIndex = 0;

    private void Awake()
    {
        for(int i = 0; i < tabButtons.Length; i++)
        {
            if(i == activeTabIndex)
            {
                tabButtons[i].interactable = false;
                tabObjects[i].SetActive(true);
            }
            else
            {
                tabButtons[i].interactable = true;
                tabObjects[i].SetActive(false);
            }
        }
    }

    public void Select(Button tabButton)
    {
        int newActiveTabIndex = 0;
        for(int i = 0; i < tabButtons.Length; i++)
        {
            if(tabButtons[i] == tabButton)
            {
                newActiveTabIndex = i;
                break;
            }
        }

        tabButtons[activeTabIndex].interactable = true;
        tabObjects[activeTabIndex].SetActive(false);

        tabButtons[newActiveTabIndex].interactable = false;
        tabObjects[newActiveTabIndex].SetActive(true);

        activeTabIndex = newActiveTabIndex;
    }
}
