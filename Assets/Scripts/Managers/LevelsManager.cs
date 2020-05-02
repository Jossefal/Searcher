using UnityEngine;
using UnityEngine.SceneManagement;

#pragma warning disable 649

public class LevelsManager : MonoBehaviour
{
    public void LoadLevel()
    {
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
