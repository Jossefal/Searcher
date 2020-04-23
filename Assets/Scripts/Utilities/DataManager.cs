using System;
using System.Text;
using UnityEngine;

public static class DataManager
{
    internal static SafeInt record;
    internal static SafeInt livesCount;

    internal static bool isHaveLocalSaveData
    {
        get
        {
            return PlayerPrefs.HasKey(Prefs.SAVE_DATA_PREF);
        }
    }

    internal static void LocalSave()
    {
        SaveData saveData = new SaveData(record.GetValue(), livesCount.GetValue());

        SafePrefs.Save(Prefs.SAVE_DATA_PREF, JsonUtility.ToJson(saveData));
    }

    internal static void LocalLoad()
    {
        SaveData saveData = JsonUtility.FromJson<SaveData>(SafePrefs.Load(Prefs.SAVE_DATA_PREF));

        if (saveData != null)
        {
            record = new SafeInt(saveData.record);
            livesCount = new SafeInt(saveData.livesCount);
        }
        else
        {
            record = new SafeInt(0);
            livesCount = new SafeInt(150);
        }
    }

    internal static void CloudSave()
    {
        SaveData saveData = new SaveData(record.GetValue(), livesCount.GetValue());

        GPGSManager.WriteSaveData(Encoding.UTF8.GetBytes(JsonUtility.ToJson(saveData)));
    }

    internal static void CloudLoad(Action onDataLoaded)
    {
        GPGSManager.ReadSaveData(GPGSManager.DEFAULT_SAVE_NAME, (status, data) =>
        {
            if (status == GooglePlayGames.BasicApi.SavedGame.SavedGameRequestStatus.Success && data.Length > 0)
            {
                SaveData saveData = JsonUtility.FromJson<SaveData>(Encoding.UTF8.GetString(data));
                record = new SafeInt(saveData.record);
                livesCount = new SafeInt(saveData.livesCount);
            }
            else
            {
                record = new SafeInt(0);
                livesCount = new SafeInt(150);
            }

            onDataLoaded.Invoke();
        });
    }

    internal static void LocalAndCloudSave()
    {
        SaveData saveData = new SaveData(record.GetValue(), livesCount.GetValue());
        string stringSaveData = JsonUtility.ToJson(saveData);

        SafePrefs.Save(Prefs.SAVE_DATA_PREF, stringSaveData);
        GPGSManager.WriteSaveData(Encoding.UTF8.GetBytes(stringSaveData));
    }
}
