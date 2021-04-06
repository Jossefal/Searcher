using UnityEngine;

internal class Prefs
{
    public const string SAVE_DATA_PREF = "SAVE_DATA";
    public const string FIRST_GPG_AUTH_CHECK_PREF = "FIRST_GPG_AUTH_CHECK";
    public const string DATE_OF_BIRTH_PREF = "DATE_OF_BIRTH";

    public static void SetBoolPref(string name, bool value)
    {
        PlayerPrefs.SetInt(name, value == true ? 1 : 0);
    }

    public static bool GetBoolPref(string name, bool defaultValue)
    {
        return PlayerPrefs.GetInt(name, defaultValue ? 1 : 0) != 0 ? true : false;
    }
}
