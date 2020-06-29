using System;
using GoogleMobileAds.Api;

public static class RewardedAdManager
{
    public static bool isLoading { get; private set; }
    public static bool isLoaded
    {
        get
        {
            if (rewardedAd == null)
                return false;
            else
                return rewardedAd.IsLoaded();
        }
    }

    public static event Action OnAdLoaded;
    public static event Action OnAdFailedToLoad;
    public static event Action OnAdFailedToShow;
    public static event Action OnUserEarnedReward;
    public static event Action OnAdClosed;

    private static RewardedAd rewardedAd;
    private const string REWARDED_AD_ID = "ca-app-pub-9489981556175219/2002368964";

    public static void ShowAd()
    {
        rewardedAd.Show();
    }

    public static void CreateAndRequestAd()
    {
        if (DataManager.isTestMode)
        {
            HandleRewardedAdFailedToLoad(null, null);
            return;
        }

        isLoading = true;

        rewardedAd = new RewardedAd(REWARDED_AD_ID);

        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
    }

    public static void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        isLoading = false;

        if (OnAdLoaded != null)
            OnAdLoaded.Invoke();
    }

    public static void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        isLoading = false;

        if (OnAdFailedToLoad != null)
            OnAdFailedToLoad.Invoke();
    }

    public static void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        if (OnAdFailedToShow != null)
            OnAdFailedToShow.Invoke();
    }

    public static void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        CreateAndRequestAd();

        if (OnAdClosed != null)
            OnAdClosed.Invoke();
    }

    public static void HandleUserEarnedReward(object sender, Reward args)
    {
        if (OnUserEarnedReward != null)
            OnUserEarnedReward.Invoke();
    }
}
