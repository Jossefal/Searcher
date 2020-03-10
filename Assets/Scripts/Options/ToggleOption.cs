using UnityEngine;
using UnityEngine.UI;

public class ToggleOption : MonoBehaviour
{
    public string optionName;
    public bool defaultValue = true;
    public InterfaceManager interfaceManager;

    public void Awake()
    {
        GetComponent<Toggle>().isOn = interfaceManager.GetBoolPref(optionName, defaultValue);
    }

    public void Save(bool value)
    {
        interfaceManager.SetBoolPref(optionName, value);
    }
}
