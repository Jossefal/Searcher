using UnityEngine;

public static class DataManager
{
    internal static SafeInt record;
    internal static SafeInt livesCount;

    public static void Save()
    {
        SaveData saveData = new SaveData(record.GetValue(), livesCount.GetValue());

        SafePrefs.Save(Prefs.SAVE_DATA_PREF, JsonUtility.ToJson(saveData));
    }

    public static void Load()
    {
        SaveData saveData = JsonUtility.FromJson<SaveData>(SafePrefs.Load(Prefs.SAVE_DATA_PREF));

        if(saveData != null)
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
}
