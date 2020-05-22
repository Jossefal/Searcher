using System;
using UnityEngine;
using GoogleMobileAds.Api;

#pragma warning disable 649

public class RespawnRewardedAdUI : AdUI
{
    [SerializeField] private RespawnPanel respawnPanel;

    private bool isNeedToShow;

    private void Start()
    {
        RewardedAdManager.OnAdLoaded += HandleAdLoaded;
        RewardedAdManager.OnAdFailedToLoad += HandleAdFailedToLoad;
        RewardedAdManager.OnAdFailedToShow += HandleAdFailedToShow;
        RewardedAdManager.OnUserEarnedReward += HandleUserEarnedReward;
        RewardedAdManager.OnAdClosed += HandleAdClosed;
    }

    public void LoadAndShowAd()
    {
        respawnPanel.PauseCooldown();
        statusText.text = "Loading ad...";
        closeBtn.SetActive(false);
        isNeedToShow = false;
        statusPanel.SetActive(true);

        if (RewardedAdManager.isLoaded)
            RewardedAdManager.ShowAd();
        else if (RewardedAdManager.isLoading)
            isNeedToShow = true;
        else
        {
            isNeedToShow = true;
            RewardedAdManager.CreateAndRequestAd();
        }
    }

    private void HandleAdLoaded()
    {
        if (isNeedToShow)
        {
            RewardedAdManager.ShowAd();
            isNeedToShow = false;
        }
    }

    private void HandleAdFailedToLoad()
    {
        isNeedToShow = false;
        statusText.text = "Loading failed";
        closeBtn.SetActive(true);
    }

    private void HandleAdFailedToShow()
    {
        isNeedToShow = false;
        statusText.text = "Showing failed";
        closeBtn.SetActive(true);
    }

    private void HandleAdClosed()
    {
        isNeedToShow = false;
        Close();
    }

    private void HandleUserEarnedReward()
    {
        respawnPanel.RespawnAfterCooldown(5f);
    }

    public void Close()
    {
        respawnPanel.ResumeCooldown();
        statusPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        RewardedAdManager.OnAdLoaded -= HandleAdLoaded;
        RewardedAdManager.OnAdFailedToLoad -= HandleAdFailedToLoad;
        RewardedAdManager.OnAdFailedToShow -= HandleAdFailedToShow;
        RewardedAdManager.OnUserEarnedReward -= HandleUserEarnedReward;
        RewardedAdManager.OnAdClosed -= HandleAdClosed;
    }
}
