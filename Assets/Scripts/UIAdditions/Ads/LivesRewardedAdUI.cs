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
        isNeedToShow = false;
        statusText.text = "Loading failed";
        closeBtn.SetActive(true);
    }

    public void HandleAdFailedToShow()
    {
        isNeedToShow = false;
        statusText.text = "Loading failed";
        closeBtn.SetActive(true);
    }

    public void HandleAdClosed()
    {
        isNeedToShow = false;
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
