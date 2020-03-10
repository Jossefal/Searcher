using UnityEngine;
using UnityEngine.UI;

public class InputFieldOption : MonoBehaviour
{
    public string optionName;
    public OptionType optionType;
    public string defaultValue;
    public InputField inputField;

    public enum OptionType
    {
        Int,
        Float,
        String
    }

    private void Awake()
    {
        switch (optionType)
        {
            case OptionType.Int:
                inputField.text = Converter.ConvertToString(PlayerPrefs.GetInt(optionName, Converter.ConvertToInt32(defaultValue)));
                break;
            case OptionType.Float:
                inputField.text = Converter.ConvertToString(PlayerPrefs.GetFloat(optionName, Converter.ConvertToFloat(defaultValue)));
                break;
            case OptionType.String:
                inputField.text = PlayerPrefs.GetString(optionName, defaultValue);
                break;
        }
    }

    public void Save()
    {
        switch (optionType)
        {
            case OptionType.Int:
                PlayerPrefs.SetInt(optionName, Converter.ConvertToInt32(inputField.text));
                break;
            case OptionType.Float:
                PlayerPrefs.SetFloat(optionName, Converter.ConvertToFloat(inputField.text));
                break;
            case OptionType.String:
                PlayerPrefs.SetString(optionName, inputField.text);
                break;
        }
    }
}
