using UnityEngine;
using UnityEngine.SceneManagement;

#pragma warning disable 649

public class LevelsManager : MonoBehaviour
{
    public void LoadLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void LoadLevel(string levelName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelName);
    }

    public void LoadStartMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
