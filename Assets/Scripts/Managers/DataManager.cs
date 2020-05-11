using System;
using System.Text;
using UnityEngine;

public static class DataManager
{
    internal static SafeInt record;
    internal static SafeInt livesCount;
    internal static uint leftToShowAd;
    internal const uint MAX_LEFT_TO_SHOW_AD = 5;

    internal static bool isHaveLocalSaveData
    {
        get
        {
            return PlayerPrefs.HasKey(Prefs.SAVE_DATA_PREF);
        }
    }

    internal static bool isDataLoaded { get; private set; }

    static DataManager()
    {
        leftToShowAd = MAX_LEFT_TO_SHOW_AD;
    }
    
    internal static void LocalSave()
    {
        SaveData saveData = new SaveData(record.GetValue(), livesCount.GetValue());

        SafePrefs.Save(Prefs.SAVE_DATA_PREF, JsonUtility.ToJson(saveData));
    }

    internal static void LocalLoad()
    {
        if (PlayerPrefs.HasKey(Prefs.SAVE_DATA_PREF))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(SafePrefs.Load(Prefs.SAVE_DATA_PREF));

            if (saveData != null)
            {
                record = new SafeInt(saveData.record);
                livesCount = new SafeInt(saveData.livesCount);
            }
            else
                LoadDefaultData();
        }
        else
            LoadDefaultData();

        isDataLoaded = true;
    }

    private static void LoadDefaultData()
    {
        record = new SafeInt(0);
        livesCount = new SafeInt(150);
    }

    internal static void CloudSave()
    {
        SaveData saveData = new SaveData(record.GetValue(), livesCount.GetValue());

        GPGSManager.WriteSaveData(GPGSManager.SAVE_FILE_NAME, Encoding.UTF8.GetBytes(JsonUtility.ToJson(saveData)));
    }

    internal static void CloudLoad(Action<bool> onDataLoaded)
    {
        GPGSManager.ReadSaveData(GPGSManager.SAVE_FILE_NAME, (status, data) =>
        {
            if (status == GooglePlayGames.BasicApi.SavedGame.SavedGameRequestStatus.Success && data.Length > 0)
            {
                SaveData saveData = JsonUtility.FromJson<SaveData>(Encoding.UTF8.GetString(data));
                record = new SafeInt(saveData.record);
                livesCount = new SafeInt(saveData.livesCount);
            }
            else
                LoadDefaultData();

            isDataLoaded = true;
            onDataLoaded.Invoke(data.Length > 0);
        });
    }

    internal static void LocalAndCloudSave()
    {
        SaveData saveData = new SaveData(record.GetValue(), livesCount.GetValue());
        string stringSaveData = JsonUtility.ToJson(saveData);

        SafePrefs.Save(Prefs.SAVE_DATA_PREF, stringSaveData);
        GPGSManager.WriteSaveData(GPGSManager.SAVE_FILE_NAME, Encoding.UTF8.GetBytes(stringSaveData));
    }
}
