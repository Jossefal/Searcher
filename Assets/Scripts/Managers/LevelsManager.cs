using UnityEngine;
using UnityEngine.SceneManagement;

#pragma warning disable 649

public class LevelsManager : MonoBehaviour
{
    public static void LoadLevelStatic()
    {
        AppManager.Play();
        SceneManager.LoadScene(2);
    }

    public static void LoadLevelStatic(string levelName)
    {
        DataManager.leftToShowAd--;
        AppManager.Play();
        SceneManager.LoadScene(levelName);
    }

    public static void LoadStartMenuStatic()
    {
        AppManager.Play();
        SceneManager.LoadScene(1);
    }
    public void LoadLevel()
    {
        DataManager.leftToShowAd--;
        AppManager.Play();
        SceneManager.LoadScene(2);
    }

    public void LoadLevel(string levelName)
    {
        AppManager.Play();
        SceneManager.LoadScene(levelName);
    }

    public void LoadStartMenu()
    {
        AppManager.Play();
        SceneManager.LoadScene(1);
    }
}
