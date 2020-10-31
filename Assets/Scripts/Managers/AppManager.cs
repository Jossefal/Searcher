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
            DataManager.LocalAndCloudSave(null);

            if (!isPaused)
                onApplicationPause.Invoke();
        }
    }

    private void OnDestroy()
    {
        DataManager.LocalSave();
    }

    private void OnApplicationQuit()
    {
        DataManager.LocalAndCloudSave(null);
    }

    public void ReportScore()
    {
        GPGSManager.ReportScore(DataManager.record.GetValue());
    }

    public void ShowLeaderBoardUI()
    {
        GPGSManager.ShowLeaderBoardUI();
    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }
}
