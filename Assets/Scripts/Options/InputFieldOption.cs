using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 649

public class InputFieldOption : MonoBehaviour
{
    [SerializeField] private string optionName;
    [SerializeField] private OptionType optionType;
    [SerializeField] private string defaultValue;
    [SerializeField] private InputField inputField;

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
