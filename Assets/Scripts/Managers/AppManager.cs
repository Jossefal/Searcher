using UnityEngine;
using UnityEngine.Events;

#pragma warning disable 649

public class AppManager : MonoBehaviour
{
    public static bool isPaused { get; private set; }
    public static bool isFirstLaunch
    {
        get
        {
            return !PlayerPrefs.HasKey(Prefs.SAVE_DATA_PREF);
        }
    }

    [SerializeField] private ScoreText scoreText;
    [SerializeField] private LivesText livesText;
    [SerializeField] private UnityEvent onApplicationPause;

    public void Awake()
    {
        isPaused = false;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        if(!DataManager.isDataLoaded)
            DataManager.LocalLoad();

        if (!GPGSManager.isAuthenticated)
        {
            GPGSManager.Initialize(false);
            GPGSManager.Auth((success) =>
            {
                if (success && !DataManager.isHaveLocalSaveData)
                    DataManager.CloudLoad(() => 
                    {
                        scoreText?.Show();
                        livesText?.Show();
                    });
                
                GPGSManager.OpenSaveData();
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
        if (pauseStatus)
        {
            DataManager.LocalAndCloudSave();

            if (!isPaused)
                onApplicationPause.Invoke();
        }
        else
            GPGSManager.OpenSaveData();
    }

    private void OnDestroy()
    {
        DataManager.LocalSave();
    }

    private void OnApplicationQuit()
    {
        DataManager.LocalAndCloudSave();
    }
}
