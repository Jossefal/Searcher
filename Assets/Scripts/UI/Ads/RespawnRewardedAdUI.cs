using System.Collections;
using UnityEngine;

#pragma warning disable 649

public class RespawnRewardedAdUI : AdUI
{
    public bool adInProcces { get; private set; }

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
        if (DataManager.isLocalTestMode)
        {
            HandleUserEarnedReward();
            HandleAdClosed();
            return;
        }

        if (adInProcces)
            return;

        adInProcces = true;
        respawnPanel.PauseCooldown();
        statusText.text = "Loading ad...";
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

    public void LoadAndShowAd(float delay)
    {
        if (DataManager.isTestMode)
        {
            HandleUserEarnedReward();
            HandleAdClosed();
            return;
        }

        if (adInProcces)
            return;

        adInProcces = true;
        respawnPanel.PauseCooldown();
        statusText.text = "Loading ad...";
        isNeedToShow = false;
        statusPanel.SetActive(true);

        if (!RewardedAdManager.isLoaded && !RewardedAdManager.isLoading)
            RewardedAdManager.CreateAndRequestAd();

        StartCoroutine(LoadAndShowWithDelay(delay));
    }

    private IEnumerator LoadAndShowWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

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
        if (isNeedToShow)
        {
            isNeedToShow = false;
            statusText.text = "Loading failed";
        }

        adInProcces = false;
    }

    private void HandleAdFailedToShow()
    {
        isNeedToShow = false;
        statusText.text = "Showing failed";
        adInProcces = false;
    }

    private void HandleAdClosed()
    {
        Close();
    }

    private void HandleUserEarnedReward()
    {
        respawnPanel.RespawnAfterCooldown(5f);
    }

    public void Close()
    {
        respawnPanel.ResumeCooldown();
        StopAllCoroutines();
        adInProcces = false;
        isNeedToShow = false;
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
