using UnityEngine;

public static class DataManager
{
    internal static SafeInt record;
    internal static SafeInt keyCount;

    public static void Save()
    {
        SaveData saveData = new SaveData(record.GetValue(), keyCount.GetValue());

        SafePrefs.Save(Prefs.SAVE_DATA_PREF, JsonUtility.ToJson(saveData));
    }

    public static void Load()
    {
        SaveData saveData = JsonUtility.FromJson<SaveData>(SafePrefs.Load(Prefs.SAVE_DATA_PREF));

        if(saveData != null)
        {
            record = new SafeInt(saveData.record);
            keyCount = new SafeInt(saveData.keyCount);
        }
        else
        {
            record = new SafeInt(0);
            keyCount = new SafeInt(150);
        }
    }
}
