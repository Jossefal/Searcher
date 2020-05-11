using System;
using UnityEngine;
using GoogleMobileAds.Api;

#pragma warning disable 649

public class RespawnRewardedAdUI : AdUI
{
    [SerializeField] private RespawnPanel respawnPanel;

    private RewardedAd rewardedAd;
    private bool isNeedToShow;
    private bool isLoading;

    private const string REWARDED_AD_ID = "ca-app-pub-3940256099942544/5224354917";

    private void Start()
    {
        CreateAndRequestAd();
    }

    public void LoadAndShowAd()
    {
        respawnPanel.PauseCooldown();
        statusPanel.SetActive(true);

        if (rewardedAd.IsLoaded())
            rewardedAd.Show();
        else if (isLoading)
            isNeedToShow = true;
        else
        {
            isNeedToShow = true;
            CreateAndRequestAd();
        }
    }

    private void CreateAndRequestAd()
    {
        respawnPanel.PauseCooldown();

        statusText.text = "Loading ad...";
        closeBtn.SetActive(false);
        isNeedToShow = false;
        isLoading = true;

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
        isLoading = false;

        if (isNeedToShow)
            rewardedAd.Show();
    }

    private void HandleAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        isLoading = false;

        statusText.text = "Failed to load";
        closeBtn.SetActive(true);
    }

    private void HandleAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        statusText.text = "Failed to show";
        closeBtn.SetActive(true);
    }

    private void HandleAdClosed(object sender, EventArgs args)
    {
        statusPanel.SetActive(false);
        CreateAndRequestAd();
    }

    private void HandleUserEarnedReward(object sender, Reward args)
    {
        respawnPanel.Respawn();
    }

    public void Close()
    {
        statusPanel.SetActive(false);
        respawnPanel.ResumeCooldown();
    }
}
