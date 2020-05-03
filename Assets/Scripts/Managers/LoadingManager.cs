using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#pragma warning disable 649

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private GameObject chooseDataPanel;
    [SerializeField] private Text cloudRecordText;
    [SerializeField] private Text cloudLivesText;
    [SerializeField] private Text localRecordText;
    [SerializeField] private Text localLivesText;

    [SerializeField] private SafeInt cloudRecord;
    [SerializeField] private SafeInt cloudLives;

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        GPGSManager.Initialize(false);

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            DataManager.LocalLoad();
            StartCoroutine(LoadStartMenu());
            return;
        }

        GPGSManager.Auth((success) =>
        {
            if (success && GPGSManager.isFirstAuth)
            {
                DataManager.CloudLoad((isExist) =>
                {
                    GPGSManager.OpenSaveData();

                    if (isExist && DataManager.isHaveLocalSaveData)
                    {
                        cloudRecord = DataManager.record;
                        cloudLives = DataManager.livesCount;
                        cloudRecordText.text = Converter.ConvertToString(cloudRecord.GetValue());
                        cloudLivesText.text = Converter.ConvertToString(cloudLives.GetValue());
                        DataManager.LocalLoad();
                        localRecordText.text = Converter.ConvertToString(DataManager.record.GetValue());
                        localLivesText.text = Converter.ConvertToString(DataManager.livesCount.GetValue());
                        chooseDataPanel.SetActive(true);
                    }
                    else if (DataManager.isHaveLocalSaveData)
                    {
                        DataManager.LocalLoad();
                        SceneManager.LoadScene(1);
                    }
                    else
                        SceneManager.LoadScene(1);
                });
            }
            else
            {
                GPGSManager.OpenSaveData();
                DataManager.LocalLoad();
                StartCoroutine(LoadStartMenu());
            }
        });
    }

    private IEnumerator LoadStartMenu()
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(1);
    }

    public void ChooseCloud()
    {
        DataManager.record = cloudRecord;
        DataManager.livesCount = cloudLives;
        SceneManager.LoadScene(1);
    }

    public void ChooseLocal()
    {
        SceneManager.LoadScene(1);
    }
}
