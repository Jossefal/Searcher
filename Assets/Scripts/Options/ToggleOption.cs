using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 649

public class ToggleOption : MonoBehaviour
{
    [SerializeField] private string optionName;
    [SerializeField] private bool defaultValue = true;
    [SerializeField] private InterfaceManager interfaceManager;

    public void Awake()
    {
        GetComponent<Toggle>().isOn = Prefs.GetBoolPref(optionName, defaultValue);
    }

    public void Save(bool value)
    {
        Prefs.SetBoolPref(optionName, value);
    }
}
