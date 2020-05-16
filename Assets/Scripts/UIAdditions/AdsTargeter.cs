using System;
using UnityEngine;
using GoogleMobileAds.Api;

#pragma warning disable 649

public class AdsTargeter : MonoBehaviour
{
    [SerializeField] private GameObject birthDatePanel;
    [SerializeField] private TextChooser monthChooser;
    [SerializeField] private TextChooser yearChooser;

    public void CheckTargetAge()
    {
        if (PlayerPrefs.HasKey(Prefs.DATE_OF_BIRTH_PREF))
        {
            DateTime dateOfBirth = DateTime.Parse(SafePrefs.Load(Prefs.DATE_OF_BIRTH_PREF));
            SetTargetAge(dateOfBirth);
            LevelsManager.LoadStartMenuStatic();
        }
        else
            birthDatePanel.SetActive(true);
    }

    public void ChooseAge()
    {
        DateTime dateOfBirth = new DateTime(Converter.ConvertToInt32(yearChooser.currentValue), monthChooser.currentIndex + 1, 1);
        SafePrefs.Save(Prefs.DATE_OF_BIRTH_PREF, dateOfBirth.ToString());
        SetTargetAge(dateOfBirth);
        LevelsManager.LoadStartMenuStatic();
    }

    private void SetTargetAge(DateTime dateOfBirth)
    {
        DateTime dateNow = DateTime.Now;
        int age = dateNow.Year - dateOfBirth.Year;
        if (dateNow.Month <= dateOfBirth.Month)
            age--;

        MaxAdContentRating maxAdContentRating;

        if (age < 7)
            maxAdContentRating = MaxAdContentRating.G;
        else if (age < 12)
            maxAdContentRating = MaxAdContentRating.PG;
        else if (age < 16)
            maxAdContentRating = MaxAdContentRating.T;
        else
            maxAdContentRating = MaxAdContentRating.MA;

        RequestConfiguration requestConfiguration = new RequestConfiguration.Builder().SetMaxAdContentRating(maxAdContentRating).build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

        RewardedAdManager.CreateAndRequestAd();
        InterstitialAdManager.CreateAndRequestAd();
    }
}
