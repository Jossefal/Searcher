using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

#pragma warning disable 649

public class LoadingManager : MonoBehaviour
{
    public MaxAdContentRating maxAdContentRating
    {
        get
        {
            return _maxAdContentRating;
        }
        set
        {
            _maxAdContentRating = value;
        }
    }

    [SerializeField] private AdsTargeter adsTargeter;
    [SerializeField] private GameObject chooseDataPanel;
    [SerializeField] private Text cloudRecordText;
    [SerializeField] private Text cloudLivesText;
    [SerializeField] private Text localRecordText;
    [SerializeField] private Text localLivesText;
    [SerializeField] private SafeInt cloudRecord;
    [SerializeField] private SafeInt cloudLives;

    private bool adsIsReady;
    private bool purchasesIsReady;
    private bool firebaseIsReady;
    private MaxAdContentRating _maxAdContentRating = MaxAdContentRating.G;

    public void Load()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        if (DataManager.isLocalTestMode)
        {
            DataManager.LocalLoad();
            LevelsManager.LoadStartMenuStatic();
            return;
        }

        GPGSManager.Initialize(false);
        FirestoreManager.Initialize();

        MobileAds.Initialize((status) =>
        {
            RequestConfiguration requestConfiguration = new RequestConfiguration.Builder().SetMaxAdContentRating(_maxAdContentRating).build();
            MobileAds.SetRequestConfiguration(requestConfiguration);

            RewardedAdManager.CreateAndRequestAd();
            InterstitialAdManager.CreateAndRequestAd();

            adsIsReady = true;
        });

        IAPManager.Initialize((status) => purchasesIsReady = true);

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            DataManager.LocalLoad();
            StartCoroutine(LoadStartMenu());
            return;
        }

        GPGSManager.Auth((success) =>
        {
            if (success)
            {
                FirestoreManager.Auth(GPGSManager.GetServerAuthCode(), task =>
                {
                    firebaseIsReady = true;
                });
            }
            else
                firebaseIsReady = true;

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
        while (!adsIsReady || !purchasesIsReady || !firebaseIsReady)
        {
            yield return null;
        }

        LevelsManager.LoadStartMenuStatic();
    }

    public void ChooseCloud()
    {
        DataManager.record = cloudRecord;
        DataManager.livesCount = cloudLives;

        StartCoroutine(LoadStartMenu());
    }

    public void ChooseLocal()
    {
        StartCoroutine(LoadStartMenu());
    }
}
