using System;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

#pragma warning disable 649

public class RespawnRewardedAdUI : AdUI
{
    [SerializeField] private RespawnPanel respawnPanel;

    private RewardedAd rewardedAd;
    // private bool isNeedToShow;
    // private bool isLoading;

    private const string REWARDED_AD_ID = "ca-app-pub-3940256099942544/5224354917";

    // private void Start()
    // {
        //CreateAndRequestAd();
    // }

    public void LoadAndShowAd()
    {
        respawnPanel.PauseCooldown();
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

        rewardedAd.OnAdLoaded += HandleAdLoaded;
        rewardedAd.OnAdFailedToLoad += HandleAdFailedToLoad;
        rewardedAd.OnAdFailedToShow += HandleAdFailedToShow;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardedAd.OnAdClosed += HandleAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
    }

    private void HandleAdLoaded(object sender, EventArgs args)
    {
        // isLoading = false;

        // if (isNeedToShow)
        rewardedAd.Show();
    }

    private void HandleAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        // isLoading = false;

        statusText.text = "Loading failed";
        closeBtn.SetActive(true);
    }

    private void HandleAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        statusText.text = "Showing failed";
        closeBtn.SetActive(true);
    }

    private void HandleAdClosed(object sender, EventArgs args)
    {
        Close();
        // CreateAndRequestAd();
    }

    private void HandleUserEarnedReward(object sender, Reward args)
    {
        respawnPanel.RespawnAfterCooldown(5f);
    }

    public void Close()
    {
        respawnPanel.ResumeCooldown();
        statusPanel.SetActive(false);
    }
}
