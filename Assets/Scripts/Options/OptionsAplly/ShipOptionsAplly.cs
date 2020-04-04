using UnityEngine;

public class ShipOptionsAplly : MonoBehaviour
{
    public MovingController mc;
    public ControlController cc;
    void Awake()
    {
        mc.maxAngle = (uint)PlayerPrefs.GetInt("MaxAngle", 45);
        mc.rotatingSpeed = PlayerPrefs.GetFloat("RotationSpeed", 0.3f);
        
        if(cc != null)
            cc.maxOffsetX = PlayerPrefs.GetFloat("MaxOffset", 1f);
    }
}
