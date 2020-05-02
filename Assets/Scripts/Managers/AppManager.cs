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
        Play();
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

    public void LogLeaderBoard()
    {
        GPGSManager.LogLeaderBoard();
    }

    public void ReportScore()
    {
        GPGSManager.ReportScore(DataManager.record.GetValue());
    }

    public void ShowLeaderBoard()
    {
        GPGSManager.ShowLeaderBoardUI();
    }
}
