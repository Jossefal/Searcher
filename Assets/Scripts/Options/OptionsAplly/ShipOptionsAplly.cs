using UnityEngine;

#pragma warning disable 649

public class ShipOptionsAplly : MonoBehaviour
{
    public MovingController mc;
    public ControlController cc;
    void Awake()
    {
        mc.maxAngle = (uint)PlayerPrefs.GetInt("MaxAngle", 45);
        mc.rotatingSpeed = PlayerPrefs.GetFloat("RotationSpeed", 0.3f);

        cc.maxOffsetX = PlayerPrefs.GetFloat("MaxOffsetXType2", 1f);
    }
}
