using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

#pragma warning disable 649

public class AppManager : MonoBehaviour
{
    public static bool isPaused { get; private set; }
    public static bool isFirstLaunch { get { return !PlayerPrefs.HasKey(FIRST_LAUNCH_PREF); } }

    [SerializeField] private ScoreText scoreText;
    [SerializeField] private LivesText livesText;
    [SerializeField] private UnityEvent onApplicationPause;

    private static string FIRST_LAUNCH_PREF = "FirstLaunch";
    
    public void Awake()
    {
        isPaused = false;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        DataManager.LocalLoad();
        
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            GPGSManager.Initialize(false);
            GPGSManager.Auth((success) =>
            {
                if (success && !DataManager.isHaveLocalSaveData)
                {
                    DataManager.CloudLoad(() => 
                    {
                        scoreText.Show();
                        livesText.Show();
                    });
                }
            });
        }
    }

    public static void Pause()
    {
        if (!isPaused)
        {
            Time.timeScale = 0f;
            isPaused = true;
        }
    }

    public static void Play()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
            isPaused = false;
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus && !isPaused)
            onApplicationPause.Invoke();
    }

    private void OnApplicationQuit()
    {
        DataManager.LocalAndCloudSave();
    }
}
