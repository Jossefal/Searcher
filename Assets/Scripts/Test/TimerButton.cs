using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TimerButton : MonoBehaviour
{
    private static DateTime lastClickTime = new DateTime(2000, 1, 1);
    private static TimeSpan timeLeft;
    private TimeSpan timeBetweenClicks = new TimeSpan(0, 10, 0);

    private static WaitForSeconds waitForOneSecond = new WaitForSeconds(1f);

    [SerializeField] private string id;
    [SerializeField] private Text text;
    [SerializeField] private GameObject normalStateObject;
    [SerializeField] private GameObject hidePanel;

    private Button button;

    private string timesSavePref
    {
        get
        {
            return "TIMER_BUTTON_TIMES_" + id;
        }
    }

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(new UnityAction(Click));
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
        text.gameObject.SetActive(false);
        normalStateObject.SetActive(true);

        button.interactable = true;
    }

    private void DisallowClick()
    {
        text.gameObject.SetActive(true);
        normalStateObject.SetActive(false);

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
        SafePrefs.Save(timesSavePref, lastClickTime.ToString());
    }

    private void Load()
    {
        string timesStr = SafePrefs.Load(timesSavePref);

        if (timesStr != "")
            lastClickTime = DateTime.Parse(timesStr);
        else
            lastClickTime = new DateTime(2000, 1, 1);
    }

    private void OnDestroy()
    {
        Save();
    }
}
