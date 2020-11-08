using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class DataManager
{
    private static SafeInt _record;
    private static SafeInt _livesCount;
    private static SafeInt _diamondsCount;
    private static SafeInt _currentThemeId;
    internal static List<int> themesIds { get; } = new List<int>();

    internal static SafeInt record
    {
        get => _record;
        set
        {
            _record = value;
            onDataChanged?.Invoke();
        }
    }

    internal static SafeInt livesCount
    {
        get => _livesCount;
        set
        {
            _livesCount = value;
            onDataChanged?.Invoke();
        }
    }

    internal static SafeInt diamondsCount
    {
        get => _diamondsCount;
        set
        {
            _diamondsCount = value;
            onDataChanged?.Invoke();
        }
    }

    internal static SafeInt currentThemeId
    {
        get => _currentThemeId;
        set
        {
            _currentThemeId = value;
            onDataChanged?.Invoke();
        }
    }

    internal static uint leftToShowAd;
    internal const uint MAX_LEFT_TO_SHOW_AD = 5;

    internal static bool isHaveLocalSaveData
    {
        get => PlayerPrefs.HasKey(Prefs.SAVE_DATA_PREF);
    }

    internal static event Action onDataChanged;

    internal static bool isDataLoaded { get; private set; }
    internal static bool isTestMode { get; private set; } = true;
    internal static bool isLocalTestMode { get; private set; } = false;

    private static int saveQueue;

    static DataManager()
    {
        leftToShowAd = MAX_LEFT_TO_SHOW_AD;
    }

    internal static void LocalSave()
    {
        SaveData saveData = CreateSaveData();

        SafePrefs.Save(Prefs.SAVE_DATA_PREF, JsonUtility.ToJson(saveData));
    }

    internal static void LocalLoad()
    {
        if (!AppManager.isFirstLaunch)
        {
            string saveDataJson = SafePrefs.Load(Prefs.SAVE_DATA_PREF);
            ParseJsonSaveData(saveDataJson);
        }
        else
            LoadDefaultData();

        isDataLoaded = true;
    }

    private static void LoadDefaultData()
    {
        record = new SafeInt(0);
        livesCount = new SafeInt(15);
        diamondsCount = new SafeInt(1000);
        currentThemeId = new SafeInt(0);
    }

    internal static void CloudSave(Action<bool> callback)
    {
        SaveData saveData = CreateSaveData();

        GPGSManager.WriteSaveData(GPGSManager.SAVE_FILE_NAME, Encoding.UTF8.GetBytes(JsonUtility.ToJson(saveData)), callback);
    }

    internal static void CloudLoad(Action<bool> onDataLoaded)
    {
        GPGSManager.ReadSaveData(GPGSManager.SAVE_FILE_NAME, (status, data) =>
        {
            if (status == GooglePlayGames.BasicApi.SavedGame.SavedGameRequestStatus.Success && data.Length > 0)
            {
                string saveDataJson = Encoding.UTF8.GetString(data);
                ParseJsonSaveData(saveDataJson);
            }
            else
                LoadDefaultData();

            isDataLoaded = true;
            onDataLoaded.Invoke(data.Length > 0);
        });
    }

    internal static void LocalAndCloudSave(Action<bool> cloudSaveCallback)
    {
        SaveData saveData = CreateSaveData();

        string stringSaveData = JsonUtility.ToJson(saveData);

        SafePrefs.Save(Prefs.SAVE_DATA_PREF, stringSaveData);
        GPGSManager.WriteSaveData(GPGSManager.SAVE_FILE_NAME, Encoding.UTF8.GetBytes(stringSaveData), cloudSaveCallback);
    }

    public static bool CheckValueCount(ValueVariant valueVariant, int count)
    {
        switch (valueVariant)
        {
            case ValueVariant.LivesCount:
                return livesCount.GetValue() >= count;
            case ValueVariant.DiamondsCount:
                return diamondsCount.GetValue() >= count;
            case ValueVariant.Record:
                return record.GetValue() >= count;
            default:
                return false;
        }
    }

    private static SaveData CreateSaveData()
    {
        SaveData saveData = new SaveData();
        saveData.record = record.GetValue();
        saveData.livesCount = livesCount.GetValue();
        saveData.diamondsCount = diamondsCount.GetValue();
        saveData.currentThemeId = currentThemeId.GetValue();
        saveData.themesIds = themesIds.ToArray();

        return saveData;
    }

    private static void ParseJsonSaveData(string saveDataJson)
    {
        SaveData saveData = JsonUtility.FromJson<SaveData>(saveDataJson);

        if (saveData == null)
        {
            LoadDefaultData();
            return;
        }

        record = new SafeInt(saveData.record);
        livesCount = new SafeInt(saveData.livesCount);

        diamondsCount = saveDataJson.Contains("diamondsCount") ? new SafeInt(saveData.diamondsCount) : new SafeInt(1000);
        currentThemeId = saveDataJson.Contains("currentThemeId") ? new SafeInt(saveData.currentThemeId) : new SafeInt(0);
    }
}

public enum ValueVariant
{
    DiamondsCount,
    LivesCount,
    Record
}
