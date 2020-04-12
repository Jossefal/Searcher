using UnityEngine;
using UnityEngine.Events;

#pragma warning disable 649

public class AppManager : MonoBehaviour
{
    [SerializeField] private UnityEvent onApplicationPause;

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        DataManager.Load();
    }

    public static void Pause()
    {
        Time.timeScale = 0f;
    } 

    public static void Play()
    {
        Time.timeScale = 1f;
    } 

    private void OnApplicationPause(bool pauseStatus)
    {
        if(pauseStatus)
            onApplicationPause.Invoke();
    }
}
