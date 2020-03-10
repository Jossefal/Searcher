using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfaceManager : MonoBehaviour
{
    public ScrollObject pausePanel;

    private int openMenusCount;

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    public void LoadLevel()
    {
        Play();
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadLevel(string levelName)
    {
        Play();
        SceneManager.LoadScene(levelName);
    }
    
    public void LoadMenu()
    {
        Play();
        SceneManager.LoadScene("StartMenu");
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    } 

    public void Play()
    {
        Time.timeScale = 1f;
    } 

    private void OnApplicationPause(bool pauseStatus)
    {
        if(pauseStatus && !GetOpenMenusStatus())
        {
            Pause();
            IncreaseOpenedMenusCount();
            pausePanel.OpenDircetly();
        }
    }

    public void IncreaseOpenedMenusCount()
    {
        openMenusCount++;
    }

    public void DecreaseOpenedMenusCount()
    {
        if(openMenusCount > 0)
            openMenusCount--;
    }

    public bool GetOpenMenusStatus()
    {
        return openMenusCount != 0;
    }

    public void SetBoolPref(string name, bool value)
    {
        PlayerPrefs.SetInt(name, value == true ? 1 : 0);
    }

    public bool GetBoolPref(string name, bool defaultValue)
    {
        return PlayerPrefs.GetInt(name, defaultValue ? 1 : 0) != 0 ? true : false;
    }
}
