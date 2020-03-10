using UnityEngine;

public class OptionScaler : MonoBehaviour
{
    public string optionName;

    private void Awake()
    {
        float scale = PlayerPrefs.GetFloat(optionName, 1f);
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
