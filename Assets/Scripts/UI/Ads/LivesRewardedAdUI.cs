using System.Collections;
using UnityEngine;

#pragma warning disable 649

public class LivesRewardedAdUI : AdUI
{
    public bool adInProcces { get; private set; }

    [SerializeField] private uint rewardLivesCount;
    [SerializeField] private GameObject rewardPanel;

    private bool isNeedToShow;

    void Start()
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

    public void HandleAdLoaded()
    {
        if (isNeedToShow)
        {
            RewardedAdManager.ShowAd();
            isNeedToShow = false;
        }
    }

    public void HandleAdFailedToLoad()
    {
        if (isNeedToShow)
        {
            isNeedToShow = false;
            statusText.text = "Loading failed";
        }

        adInProcces = false;
    }

    public void HandleAdFailedToShow()
    {
        isNeedToShow = false;
        statusText.text = "Loading failed";
        adInProcces = false;
    }

    public void HandleAdClosed()
    {
        isNeedToShow = false;
        statusPanel.SetActive(false);
        adInProcces = false;
    }

    public void HandleUserEarnedReward()
    {
        rewardPanel.SetActive(true);
        DataManager.livesCount = new SafeInt(DataManager.livesCount.GetValue() + (int)rewardLivesCount);
    }

    public void Close()
    {
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
