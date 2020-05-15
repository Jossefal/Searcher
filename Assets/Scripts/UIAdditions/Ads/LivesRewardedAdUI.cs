using UnityEngine;

#pragma warning disable 649

public class LivesRewardedAdUI : AdUI
{
    [SerializeField] private LivesText livesText;
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
        statusText.text = "Loading ad...";
        closeBtn.SetActive(false);
        isNeedToShow = false;
        statusPanel.SetActive(true);

        if (RewardedAdManager.isLoaded)
            RewardedAdManager.ShowAd();
        else if (RewardedAdManager.isLoading)
            isNeedToShow = true;
        else
            RewardedAdManager.CreateAndRequestAd();
    }

    public void HandleAdLoaded()
    {
        if (isNeedToShow)
            RewardedAdManager.ShowAd();
    }

    public void HandleAdFailedToLoad()
    {
        statusText.text = "Loading failed";
        closeBtn.SetActive(true);
    }

    public void HandleAdFailedToShow()
    {
        statusText.text = "Loading failed";
        closeBtn.SetActive(true);
    }

    public void HandleAdClosed()
    {
        statusPanel.SetActive(false);
    }

    public void HandleUserEarnedReward()
    {
        rewardPanel.SetActive(true);
        DataManager.livesCount = new SafeInt(DataManager.livesCount.GetValue() + (int)rewardLivesCount);
        livesText.Show();
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
