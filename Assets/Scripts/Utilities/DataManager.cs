using UnityEngine;

public static class DataManager
{
    internal static SafeInt record;

    public static void Save()
    {
        SaveData saveData = new SaveData(record.GetValue());

        SafePrefs.Save(Prefs.RECORD_PREF, JsonUtility.ToJson(saveData));
    }

    public static void Load()
    {
        SaveData saveData = JsonUtility.FromJson<SaveData>(SafePrefs.Load(Prefs.RECORD_PREF));

        record = new SafeInt((int)saveData?.record);
    }
}
