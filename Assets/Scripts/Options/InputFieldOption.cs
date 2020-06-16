using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 649

public class InputFieldOption : MonoBehaviour
{
    [SerializeField] private string optionName;
    [SerializeField] private OptionType optionType;
    [SerializeField] private string defaultValue;

    private bool isInitializated;

    public enum OptionType
    {
        Int,
        Float,
        String
    }

    private void OnEnable()
    {
        if (isInitializated)
            return;

        InputField inputField = GetComponent<InputField>();

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

        UnityEngine.Events.UnityAction<string> saveAction = Save;
        inputField.onEndEdit.AddListener(saveAction);

        isInitializated = true;
    }

    public void Save(string value)
    {
        switch (optionType)
        {
            case OptionType.Int:
                PlayerPrefs.SetInt(optionName, Converter.ConvertToInt32(value));
                break;
            case OptionType.Float:
                PlayerPrefs.SetFloat(optionName, Converter.ConvertToFloat(value));
                break;
            case OptionType.String:
                PlayerPrefs.SetString(optionName, value);
                break;
        }
    }
}
