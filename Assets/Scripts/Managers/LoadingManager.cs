using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

#pragma warning disable 649

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private AdsTargeter adsTargeter;
    [SerializeField] private GameObject chooseDataPanel;
    [SerializeField] private Text cloudRecordText;
    [SerializeField] private Text cloudLivesText;
    [SerializeField] private Text localRecordText;
    [SerializeField] private Text localLivesText;

    [SerializeField] private SafeInt cloudRecord;
    [SerializeField] private SafeInt cloudLives;

    private bool adsIsReady;

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        GPGSManager.Initialize(false);
        MobileAds.Initialize((status) => 
        {
            adsIsReady = true;
        });

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            DataManager.LocalLoad();
            StartCoroutine(LoadStartMenu());
            return;
        }

        GPGSManager.Auth((success) =>
        {
            if (success && GPGSManager.isFirstAuth)
            {
                DataManager.CloudLoad((isExist) =>
                {
                    GPGSManager.OpenSaveData();

                    if (isExist && DataManager.isHaveLocalSaveData)
                    {
                        cloudRecord = DataManager.record;
                        cloudLives = DataManager.livesCount;
                        cloudRecordText.text = Converter.ConvertToString(cloudRecord.GetValue());
                        cloudLivesText.text = Converter.ConvertToString(cloudLives.GetValue());
                        DataManager.LocalLoad();
                        localRecordText.text = Converter.ConvertToString(DataManager.record.GetValue());
                        localLivesText.text = Converter.ConvertToString(DataManager.livesCount.GetValue());
                        chooseDataPanel.SetActive(true);
                    }
                    else if (DataManager.isHaveLocalSaveData)
                    {
                        DataManager.LocalLoad();
                        StartCoroutine(LoadStartMenu());
                    }
                    else
                        StartCoroutine(LoadStartMenu());
                });
            }
            else
            {
                GPGSManager.OpenSaveData();
                DataManager.LocalLoad();
                StartCoroutine(LoadStartMenu());
            }
        });
    }

    private IEnumerator LoadStartMenu()
    {
        while(!adsIsReady)
        {
            yield return null;
        }

        adsTargeter.CheckTargetAge();
    }

    public void ChooseCloud()
    {
        DataManager.record = cloudRecord;
        DataManager.livesCount = cloudLives;
        LevelsManager.LoadStartMenuStatic();
    }

    public void ChooseLocal()
    {
        LevelsManager.LoadStartMenuStatic();
    }
}
