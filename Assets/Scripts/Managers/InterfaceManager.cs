using UnityEngine;

#pragma warning disable 649

public class InterfaceManager : MonoBehaviour
{
    [SerializeField] private ScrollObject pausePanel;
    [SerializeField] private GameObject[] hiddenObjects;

    public void Pause()
    {
        AppManager.Pause();
    }

    public void Play()
    {
        AppManager.Play();
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
