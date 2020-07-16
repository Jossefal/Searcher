using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class DataManager
{
    private static SafeInt _record;
    private static SafeInt _livesCount;
    private static SafeInt _diamondsCount;
    private static SafeInt _currentEnvironmentSkinId;
    private static SafeInt _currentShipSkinId;
    internal static List<int> environmentSkinIds { get; } = new List<int>();
    internal static List<int> shipSkinIds { get; } = new List<int>();

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

    internal static SafeInt currentEnvironmentSkinId
    {
        get => _currentEnvironmentSkinId;
        set
        {
            _currentEnvironmentSkinId = value;
            onDataChanged?.Invoke();
        }
    }

    internal static SafeInt currentShipSkinId
    {
        get => _currentShipSkinId;
        set
        {
            _currentShipSkinId = value;
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

    internal static bool isTestMode { get; private set; } = false;
    internal static bool isLocalTestMode { get; private set; } = true;

    static DataManager()
    {
        leftToShowAd = MAX_LEFT_TO_SHOW_AD;
    }

    internal static void LocalSave()
    {
        SaveData saveData = new SaveData();
        saveData.record = record.GetValue();
        saveData.livesCount = livesCount.GetValue();
        saveData.diamondsCount = diamondsCount.GetValue();
        saveData.currentEnvironmentSkinId = currentEnvironmentSkinId.GetValue();
        saveData.currentShipSkinId = currentShipSkinId.GetValue();
        saveData.environmentSkinIds = environmentSkinIds.ToArray();
        saveData.shipSkinIds = shipSkinIds.ToArray();

        SafePrefs.Save(Prefs.SAVE_DATA_PREF, JsonUtility.ToJson(saveData));
    }

    internal static void LocalLoad()
    {
        if (PlayerPrefs.HasKey(Prefs.SAVE_DATA_PREF))
        {
            string saveDataJson = SafePrefs.Load(Prefs.SAVE_DATA_PREF);
            SaveData saveData = JsonUtility.FromJson<SaveData>(saveDataJson);

            if (saveData != null)
            {
                record = new SafeInt(saveData.record);
                livesCount = new SafeInt(saveData.livesCount);

                diamondsCount = saveDataJson.Contains("diamondsCount") ? new SafeInt(saveData.diamondsCount) : new SafeInt(5);
                currentEnvironmentSkinId = saveDataJson.Contains("currentEnvironmentSkinId") ? new SafeInt(saveData.currentEnvironmentSkinId) : new SafeInt(0);
                currentShipSkinId = saveDataJson.Contains("currentShipSkinId") ? new SafeInt(saveData.currentShipSkinId) : new SafeInt(0);

                if (saveDataJson.Contains("environmentSkinIds"))
                    environmentSkinIds.AddRange(saveData.environmentSkinIds);
                else
                    environmentSkinIds.Add(0);

                if (saveDataJson.Contains("shipSkinIds"))
                    shipSkinIds.AddRange(saveData.shipSkinIds);
                else
                    shipSkinIds.Add(0);
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
        livesCount = new SafeInt(15);
        diamondsCount = new SafeInt(1000);
        currentEnvironmentSkinId = new SafeInt(0);
        currentShipSkinId = new SafeInt(0);
        environmentSkinIds.Add(0);
        shipSkinIds.Add(0);
    }

    internal static void CloudSave(Action<bool> callback)
    {
        SaveData saveData = new SaveData();
        saveData.record = record.GetValue();
        saveData.livesCount = livesCount.GetValue();
        saveData.diamondsCount = diamondsCount.GetValue();
        saveData.currentEnvironmentSkinId = currentEnvironmentSkinId.GetValue();
        saveData.currentShipSkinId = currentShipSkinId.GetValue();
        saveData.environmentSkinIds = environmentSkinIds.ToArray();
        saveData.shipSkinIds = shipSkinIds.ToArray();

        GPGSManager.WriteSaveData(GPGSManager.SAVE_FILE_NAME, Encoding.UTF8.GetBytes(JsonUtility.ToJson(saveData)), callback);
    }

    internal static void CloudLoad(Action<bool> onDataLoaded)
    {
        GPGSManager.ReadSaveData(GPGSManager.SAVE_FILE_NAME, (status, data) =>
        {
            if (status == GooglePlayGames.BasicApi.SavedGame.SavedGameRequestStatus.Success && data.Length > 0)
            {
                string saveDataJson = SafePrefs.Load(Encoding.UTF8.GetString(data));
                SaveData saveData = JsonUtility.FromJson<SaveData>(saveDataJson);

                record = new SafeInt(saveData.record);
                livesCount = new SafeInt(saveData.livesCount);

                diamondsCount = saveDataJson.Contains("diamondsCount") ? new SafeInt(saveData.diamondsCount) : new SafeInt(5);
                currentEnvironmentSkinId = saveDataJson.Contains("currentEnvironmentSkinId") ? new SafeInt(saveData.currentEnvironmentSkinId) : new SafeInt(0);
                currentShipSkinId = saveDataJson.Contains("currentShipSkinId") ? new SafeInt(saveData.currentShipSkinId) : new SafeInt(0);

                if (saveDataJson.Contains("environmentSkinIds"))
                    environmentSkinIds.AddRange(saveData.environmentSkinIds);
                else
                    environmentSkinIds.Add(0);

                if (saveDataJson.Contains("shipSkinIds"))
                    shipSkinIds.AddRange(saveData.shipSkinIds);
                else
                    shipSkinIds.Add(0);
            }
            else
                LoadDefaultData();

            isDataLoaded = true;
            onDataLoaded.Invoke(data.Length > 0);
        });
    }

    internal static void LocalAndCloudSave(Action<bool> cloudSaveCallback)
    {
        SaveData saveData = new SaveData();
        saveData.record = record.GetValue();
        saveData.livesCount = livesCount.GetValue();
        saveData.diamondsCount = diamondsCount.GetValue();
        saveData.currentEnvironmentSkinId = currentEnvironmentSkinId.GetValue();
        saveData.currentShipSkinId = currentShipSkinId.GetValue();
        saveData.environmentSkinIds = environmentSkinIds.ToArray();
        saveData.shipSkinIds = shipSkinIds.ToArray();

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
}

public enum ValueVariant
{
    Record,
    LivesCount,
    DiamondsCount
}
