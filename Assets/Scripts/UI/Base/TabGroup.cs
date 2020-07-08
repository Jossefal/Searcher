using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#pragma warning disable 649

public class TabGroup : MonoBehaviour
{
    [Serializable]
    public class Tab
    {
        public Button button;
        public GameObject tabObject;
        public Action<Tab> onEnable;

        public void Initialize()
        {
            button.onClick.AddListener(new UnityAction(Enable));
        }

        public void Enable()
        {
            button.interactable = false;
            tabObject.SetActive(true);

            onEnable(this);
        }

        public void Disable()
        {
            button.interactable = true;
            tabObject.SetActive(false);
        }
    }

    [SerializeField] private Tab[] tabs;
    [SerializeField] private int activeTabIndex = 0;

    private void Awake()
    {
        if (tabs.Length == 0)
            return;

        for (int i = 0; i < tabs.Length; i++)
        {
            tabs[i].onEnable = DisableUnselectedTabs;
            tabs[i].Initialize();
        }

        if (activeTabIndex >= 0 && activeTabIndex < tabs.Length)
            tabs[activeTabIndex].Enable();
        else
            tabs[0].Enable();
    }

    private void DisableUnselectedTabs(Tab selectedTab)
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            if (tabs[i] != selectedTab)
                tabs[i].Disable();
        }
    }
}
