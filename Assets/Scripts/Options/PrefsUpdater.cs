using UnityEngine;

public class PrefsUpdater : MonoBehaviour
{
    public enum OptionType
    {
        Int,
        Float,
        String
    }

    [System.Serializable]
    public class UpdatedPref
    {
        public OptionType optionType;
        public string legacyName;
        public string currentName;
    }

    public UpdatedPref[] updatedPrefs;

    private void Awake()
    {
        for (int i = 0; i < updatedPrefs.Length; i++)
        {
            if (!PlayerPrefs.HasKey(updatedPrefs[i].currentName))
            {
                switch (updatedPrefs[i].optionType)
                {
                    case OptionType.Int:
                        PlayerPrefs.SetInt(updatedPrefs[i].currentName, PlayerPrefs.GetInt(updatedPrefs[i].legacyName, 0));
                        break;
                    case OptionType.Float:
                        PlayerPrefs.SetFloat(updatedPrefs[i].currentName, PlayerPrefs.GetFloat(updatedPrefs[i].legacyName, 0f));
                        break;
                    case OptionType.String:
                        PlayerPrefs.SetString(updatedPrefs[i].currentName, PlayerPrefs.GetString(updatedPrefs[i].legacyName, ""));
                        break;
                }
            }
        }
    }
}
