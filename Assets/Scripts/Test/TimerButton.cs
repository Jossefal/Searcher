using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerButton : MonoBehaviour
{
    private static DateTime lastClickTime = new DateTime(2000, 1, 1);
    private static TimeSpan timeLeft;
    private TimeSpan timeBetweenClicks = new TimeSpan(0, 2, 0);

    private static WaitForSeconds waitForOneSecond = new WaitForSeconds(1f);

    [SerializeField] private Text text;
    [SerializeField] private GameObject hidePanel;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        Load();
    }

    private void OnEnable()
    {
        hidePanel.SetActive(true);

        InternetTime.GetTime((success, dateTime) =>
        {
            if (!success)
                return;

            TimeSpan lastClickTimeSpan = dateTime.Subtract(lastClickTime);

            if (lastClickTimeSpan > timeBetweenClicks)
                AllowClick();
            else
            {
                timeLeft = timeBetweenClicks.Subtract(lastClickTimeSpan);
                DisallowClick();
                StartTimer();
            }

            hidePanel.SetActive(false);
        });
    }

    private void StartTimer()
    {
        StartCoroutine(Timer(CheckTime, AllowClick));
    }

    private IEnumerator Timer(Func<bool> func, Action final)
    {
        if (func == null)
            yield break;

        text.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeLeft.Hours, timeLeft.Minutes, timeLeft.Seconds);

        do
        {
            yield return waitForOneSecond;
        }
        while (func.Invoke());

        final?.Invoke();
    }

    private bool CheckTime()
    {
        timeLeft = timeLeft.Subtract(new TimeSpan(0, 0, 1));
        text.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeLeft.Hours, timeLeft.Minutes, timeLeft.Seconds);

        return timeLeft.Hours > 0 || timeLeft.Minutes > 0 || timeLeft.Seconds > 0;
    }

    private void AllowClick()
    {
        text.text = "Click!";
        button.interactable = true;
    }

    private void DisallowClick()
    {
        button.interactable = false;
    }

    public void Click()
    {
        DisallowClick();
        hidePanel.SetActive(true);

        InternetTime.GetTime((success, dateTime) =>
        {
            if (!success)
                return;

            lastClickTime = dateTime;
            timeLeft = timeBetweenClicks;

            StartTimer();
            hidePanel.SetActive(false);
        });
    }

    public void Save()
    {
        SafePrefs.Save("Save", lastClickTime.ToString());
    }

    private void Load()
    {
        string str = SafePrefs.Load("Save");

        if (str != "")
            lastClickTime = DateTime.Parse(str);
        else
            lastClickTime = new DateTime(2000, 1, 1);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
	if(pauseStatus)
	{
	    Save();
            Debug.Log("SAVE");
	}
    }
}
