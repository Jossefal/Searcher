using UnityEngine;

#pragma warning disable 649

public class InterfaceManager : MonoBehaviour
{
    [SerializeField] private ScrollObject pausePanel;
    [SerializeField] private GameObject[] hiddenObjects;

    private int openMenusCount;

    public void Pause()
    {
        AppManager.Pause();
    }

    public void Play()
    {
        AppManager.Play();
    }

    public void IncreaseOpenedMenusCount()
    {
        openMenusCount++;
    }

    public void DecreaseOpenedMenusCount()
    {
        if (openMenusCount > 0)
            openMenusCount--;
    }

    public bool GetOpenMenusStatus()
    {
        return openMenusCount != 0;
    }

    public void HideObjects()
    {
        for (int i = 0; i < hiddenObjects.Length; i++)
        {
            hiddenObjects[i].SetActive(false);
        }
    }

    public void ShowObjects()
    {
        for (int i = 0; i < hiddenObjects.Length; i++)
        {
            hiddenObjects[i].SetActive(true);
        }
    }
}
