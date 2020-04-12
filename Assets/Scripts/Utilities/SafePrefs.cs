using UnityEngine;

public static class SafePrefs
{
    public static void Save(string key, string value)
    {
        string encodedValue = Base64.Encode(value);

        PlayerPrefs.SetString(key, encodedValue);
    }

    public static string Load(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            string decodedValue = Base64.Decode(PlayerPrefs.GetString(key));
            return decodedValue;
        }
        else
            return "";
    }
}
