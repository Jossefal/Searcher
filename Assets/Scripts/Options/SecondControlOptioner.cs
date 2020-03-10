using UnityEngine;

public class SecondControlOptioner : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<ControlController>().maxOffsetX = PlayerPrefs.GetFloat("MaxOffsetX", 1f);
    }
}
