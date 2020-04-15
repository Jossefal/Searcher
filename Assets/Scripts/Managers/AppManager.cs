using UnityEngine;
using UnityEngine.Events;

#pragma warning disable 649

public class AppManager : MonoBehaviour
{
    public static bool isPaused { get; private set; }

    [SerializeField] private UnityEvent onApplicationPause;

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        DataManager.Load();
    }

    public static void Pause()
    {
        if(!isPaused)
        {
            Time.timeScale = 0f;
            isPaused = true;
        }      
    }

    public static void Play()
    {
        if(isPaused)
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
}
