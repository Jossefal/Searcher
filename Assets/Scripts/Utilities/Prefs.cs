using UnityEngine;

public class Prefs
{
    internal const string RECORD_PREF = "Record";

    internal const string SKIN_INDEX_PREF = "SkinIndex";

    public static void SetBoolPref(string name, bool value)
    {
        PlayerPrefs.SetInt(name, value == true ? 1 : 0);
    }

    public static bool GetBoolPref(string name, bool defaultValue)
    {
        return PlayerPrefs.GetInt(name, defaultValue ? 1 : 0) != 0 ? true : false;
    }
}
