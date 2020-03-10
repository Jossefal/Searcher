using UnityEngine;
using UnityEngine.UI;

public class SwitchOption : MonoBehaviour
{
    public string optionName;
    public OptionType optionType;
    public string defaultValue;
    public string[] values;
    public Toggle[] toggles;

    private int currentIndex;

    public enum OptionType
    {
        Int,
        Float,
        String
    }

    private void Awake()
    {
        string value = "";

        switch (optionType)
        {
            case OptionType.Int:
                value = Converter.ConvertToString(PlayerPrefs.GetInt(optionName, Converter.ConvertToInt32(defaultValue)));
                break;
            case OptionType.Float:
                value = Converter.ConvertToString(PlayerPrefs.GetFloat(optionName, Converter.ConvertToFloat(defaultValue)));
                break;
            case OptionType.String:
                value = PlayerPrefs.GetString(optionName, defaultValue);
                break;
        }

        for (int i = 0; i < values.Length; i++)
        {
            if (values[i] == value)
            {
                toggles[i].isOn = true;
                currentIndex = i;
            }
            else
                toggles[i].isOn = false;
        }
    }

    public void SelectCase(Toggle toggle)
    {
        toggle.isOn = true;

        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i] != toggle)
                toggles[i].isOn = false;
            else
                currentIndex = i;
        }

        Save();
    }

    public void Save()
    {
        switch (optionType)
        {
            case OptionType.Int:
                PlayerPrefs.SetInt(optionName, Converter.ConvertToInt32(values[currentIndex]));
                break;
            case OptionType.Float:
                PlayerPrefs.SetFloat(optionName, Converter.ConvertToFloat(values[currentIndex]));
                break;
            case OptionType.String:
                PlayerPrefs.SetString(optionName, values[currentIndex]);
                break;
        }
    }
}
