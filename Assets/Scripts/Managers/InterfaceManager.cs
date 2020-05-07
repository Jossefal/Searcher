using UnityEngine;

#pragma warning disable 649

public class InterfaceManager : MonoBehaviour
{
    [SerializeField] private GameObject[] hiddenObjects;
    [SerializeField] private GameObject ship;
    [SerializeField] private GameObject pauseBtn;
    [SerializeField] private ScrollObject pausePanel;
    [SerializeField] private Animator pauseFadePanel;
    [SerializeField] private Animator scoreText;

    public void Pause()
    {
        AppManager.Pause();
    }

    public void Play()
    {
        AppManager.Play();
    }

    public void OpenPausePanelDirectly()
    {
        if(ship.activeSelf)
        {
            AppManager.Pause();
            pausePanel.gameObject.SetActive(true);
            pausePanel.OpenDircetly();
            pauseBtn.SetActive(false);
            pauseFadePanel.SetTrigger("toOpaque");
            scoreText.SetTrigger("toWhite");
        }
    }

    public void OpenPausePanel()
    {
        if(ship.activeSelf)
        {
            AppManager.Pause();
            pausePanel.gameObject.SetActive(true);
            pausePanel.Open();
            pauseBtn.SetActive(false);
            pauseFadePanel.SetTrigger("toOpaque");
            scoreText.SetTrigger("toWhite");
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
