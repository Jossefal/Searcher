using UnityEngine;

#pragma warning disable 649

public class InterfaceManager : MonoBehaviour
{
    [SerializeField] private GameObject[] hiddenObjects;
    [SerializeField] private GameObject ship;
    [SerializeField] private ScrollObject pausePanel;
    [SerializeField] private Animator pauseFadePanel;

    public void Pause()
    {
        AppManager.Pause();
    }

    public void Play()
    {
        AppManager.Play();
    }

    public void OpenPausePanel()
    {
        if(ship.activeSelf)
        {
            AppManager.Pause();
            pausePanel.OpenDircetly();
            pauseFadePanel.SetTrigger("toOpaque");
        }
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
