using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class LevelIntersitialAdUI : AdUI
{
    public bool adIsLoaded
    {
        get
        {
            return interstitialAd.IsLoaded();
        }
    }
    
    [SerializeField] private InterstitialAd interstitialAd;

    private const string INTERSTITIAL_AD_ID = "ca-app-pub-3940256099942544/1033173712";
    private Action onClosedAd;

    private void Start()
    {
        CreateAndRequestAd();
    }

    public void ShowAd(Action onClosedAd)
    {
        this.onClosedAd = onClosedAd;
        interstitialAd.Show();
    }

    private void CreateAndRequestAd()
    {
        interstitialAd = new InterstitialAd(INTERSTITIAL_AD_ID);

        interstitialAd.OnAdClosed += HandleAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(request);
    }

    private void HandleAdClosed(object sender, EventArgs args)
    {
        statusPanel.SetActive(false);
        CreateAndRequestAd();
        onClosedAd.Invoke();
    }

    public void Close()
    {
        statusPanel.SetActive(false);
    }
}
