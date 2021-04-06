using System;
using System.Collections;
using UnityEngine;

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
        if(DataManager.isLocalTestMode)
        {
            onClosedAd?.Invoke();
        }

        this.onClosedAd = onClosedAd;
        InterstitialAdManager.ShowAd();
    }

    public void ShowAd(float delay, Action onClosedAd)
    {
        if(DataManager.isLocalTestMode)
        {
            onClosedAd?.Invoke();
        }

        this.onClosedAd = onClosedAd;
        statusPanel.SetActive(true);

        StartCoroutine(ShowAdWithDelay(delay));
    }

    private IEnumerator ShowAdWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        InterstitialAdManager.ShowAd();
    }

    private void HandleAdClosed()
    {
        statusPanel.SetActive(false);

        if (onClosedAd != null)
        {
            onClosedAd.Invoke();
            onClosedAd = null;
        }

        statusPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        InterstitialAdManager.OnAdClosed -= HandleAdClosed;
    }
}
