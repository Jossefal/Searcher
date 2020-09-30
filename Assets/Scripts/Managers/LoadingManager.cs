using System;
using System.Collections;
using UnityEngine;
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

    private bool adsIsReady;
    private bool purchasesIsReady;
    private bool firebaseIsReady;
    private bool savesIsReady;
    private MaxAdContentRating _maxAdContentRating = MaxAdContentRating.G;

    public void Load()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        if (DataManager.isLocalTestMode || Application.internetReachability == NetworkReachability.NotReachable)
        {
            DataManager.LocalLoad();
            LevelsManager.LoadStartMenuStatic();
            return;
        }

        InitializeServices();

        GooglePlayAuth((succes) =>
        {
            FirestoreAuth();

            LoadSaves();
        });

        StartCoroutine(LoadStartMenu());
    }

    private void InitializeServices()
    {
        GPGSManager.Initialize(false);

        FirestoreManager.Initialize();

        IAPManager.Initialize((status) => purchasesIsReady = true);

        MobileAds.Initialize((status) =>
        {
            RequestConfiguration requestConfiguration = new RequestConfiguration.Builder().SetMaxAdContentRating(_maxAdContentRating).build();
            MobileAds.SetRequestConfiguration(requestConfiguration);

            RewardedAdManager.CreateAndRequestAd();
            InterstitialAdManager.CreateAndRequestAd();

            adsIsReady = true;
        });
    }

    private void GooglePlayAuth(Action<bool> onAuth)
    {
        GPGSManager.Auth(onAuth);
    }

    private void FirestoreAuth()
    {
        if (GPGSManager.isAuthenticated)
        {
            string authCode = GPGSManager.GetServerAuthCode();
            FirestoreManager.Auth(authCode, task =>
            {
                firebaseIsReady = true;
            });
        }
        else
            firebaseIsReady = true;
    }

    private void LoadSaves()
    {
        if (GPGSManager.isAuthenticated && GPGSManager.isFirstAuth)
        {
            DataManager.CloudLoad((isExist) =>
            {
                if (!isExist)
                    DataManager.LocalLoad();

                savesIsReady = true;
            });
        }
        else
        {
            DataManager.LocalLoad();
            savesIsReady = true;
        }
    }

    private IEnumerator LoadStartMenu()
    {
        while (!adsIsReady || !purchasesIsReady || !firebaseIsReady || !savesIsReady)
        {
            yield return null;
        }

        LevelsManager.LoadStartMenuStatic();
    }
}
