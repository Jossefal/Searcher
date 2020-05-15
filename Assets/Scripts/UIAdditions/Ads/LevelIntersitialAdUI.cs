using System;

public class LevelIntersitialAdUI : AdUI
{
    public bool adIsLoaded
    {
        get
        {
            return InterstitialAdManager.isLoaded;
        }
    }

    private Action onClosedAd;

    private void Start()
    {
        InterstitialAdManager.OnAdClosed += HandleAdClosed;

        if (!InterstitialAdManager.isLoaded && !InterstitialAdManager.isLoading)
            InterstitialAdManager.CreateAndRequestAd();
    }

    public void ShowAd(Action onClosedAd)
    {
        this.onClosedAd = onClosedAd;
        InterstitialAdManager.ShowAd();
    }

    private void HandleAdClosed()
    {
        statusPanel.SetActive(false);

        if (onClosedAd != null)
            onClosedAd.Invoke();
    }

    public void Close()
    {
        statusPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        InterstitialAdManager.OnAdClosed -= HandleAdClosed;
    }
}
