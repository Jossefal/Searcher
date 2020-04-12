using UnityEngine;

#pragma warning disable 649

public class SecondControlOptioner : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<ControlController>().maxOffsetX = PlayerPrefs.GetFloat("MaxOffsetX", 1f);
    }
}
