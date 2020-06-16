using System;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

#pragma warning disable 649

public class AdsTargeter : MonoBehaviour
{
    [SerializeField] private GameObject birthDatePanel;
    [SerializeField] private TextChooser monthChooser;
    [SerializeField] private TextChooser yearChooser;
    [SerializeField] private Button okButton;
    [SerializeField] private LoadingManager loadingManager;

    private void Start()
    {
        CheckTargetAge();
    }

    public void CheckTargetAge()
    {
        if (PlayerPrefs.HasKey(Prefs.DATE_OF_BIRTH_PREF))
        {
            DateTime dateOfBirth = DateTime.Parse(SafePrefs.Load(Prefs.DATE_OF_BIRTH_PREF));
            SetTargetAge(dateOfBirth);
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

        loadingManager.maxAdContentRating = maxAdContentRating;
        loadingManager.Load();
    }

    public void CheckValues()
    {
        if(monthChooser.isEmptyValue || yearChooser.isEmptyValue)
            okButton.interactable = false;
        else
            okButton.interactable = true;
    }
}
