using System;
using UnityEngine;
using UnityEngine.Events;
using GoogleMobileAds.Api;

#pragma warning disable 649

public class LivesRewardedAdUI : AdUI
{
    [SerializeField] private LivesText livesText;
    [SerializeField] private uint rewardLivesCount;
    [SerializeField] private GameObject rewardPanel;

    private RewardedAd rewardedAd;
    private bool isNeedToShow;
    private bool isLoading;

    private const string REWARDED_AD_ID = "ca-app-pub-3940256099942544/5224354917";

    // void Start()
    // {
    //     CreateAndRequestAd();
    // }

    public void LoadAndShowAd()
    {
        statusPanel.SetActive(true);
        CreateAndRequestAd();

        // if (rewardedAd.IsLoaded())
        //     rewardedAd.Show();
        // else if (isLoading)
        //     isNeedToShow = true;
        // else
        // {
        //     isNeedToShow = true;
        //     CreateAndRequestAd();
        // }
    }

    private void CreateAndRequestAd()
    {
        statusText.text = "Loading ad...";
        closeBtn.SetActive(false);
        // isNeedToShow = false;
        // isLoading = true;

        rewardedAd = new RewardedAd(REWARDED_AD_ID);

        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        // isLoading = false;

        // if (isNeedToShow)
        rewardedAd.Show();
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        // isLoading = false;

        statusText.text = "Loading failed";
        closeBtn.SetActive(true);
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        statusText.text = "Loading failed";
        closeBtn.SetActive(true);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        statusPanel.SetActive(false);
        // CreateAndRequestAd();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        rewardPanel.SetActive(true);
        DataManager.livesCount = new SafeInt(DataManager.livesCount.GetValue() + (int)rewardLivesCount);
        livesText.Show();
    }
}
