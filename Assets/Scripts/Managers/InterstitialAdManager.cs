using System;
using GoogleMobileAds.Api;

public static class InterstitialAdManager
{
    public static bool isLoading { get; private set; }
    public static bool isLoaded
    {
        get
        {
            if (interstitialAd == null)
                return false;
            else
                return interstitialAd.IsLoaded();
        }
    }

    public static event Action OnAdClosed;

    private static InterstitialAd interstitialAd;
    private const string INTERSTITIAL_AD_ID = "ca-app-pub-9489981556175219/5193563577";

    public static void ShowAd()
    {
        interstitialAd.Show();
    }

    public static void CreateAndRequestAd()
    {
        isLoading = true;

        // interstitialAd = new InterstitialAd(INTERSTITIAL_AD_ID);

        // interstitialAd.OnAdClosed += HandleInterstitialAdClosed;
        // interstitialAd.OnAdLoaded += HandleInterstitialAdLoaded;
        // interstitialAd.OnAdFailedToLoad += HandleInterstitialAdFailedToLoad;

        // AdRequest request = new AdRequest.Builder().Build();
        // interstitialAd.LoadAd(request);

        HandleInterstitialAdFailedToLoad(null, null);
    }

    public static void HandleInterstitialAdLoaded(object sender, EventArgs args)
    {
        isLoading = false;
    }

    public static void HandleInterstitialAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        isLoading = false;
    }

    private static void HandleInterstitialAdClosed(object sender, EventArgs args)
    {
        CreateAndRequestAd();

        if (OnAdClosed != null)
            OnAdClosed.Invoke();
    }
}
