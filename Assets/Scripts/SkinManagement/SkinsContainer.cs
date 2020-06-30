using UnityEngine;

#pragma warning disable 649

[CreateAssetMenu(fileName = "NewSkinsContainer", menuName = "ScriptableObject`s/SkinsContainer")]
public class SkinsContainer : ScriptableObject
{
    public EnvironmentSkinData[] environmentSkins;
    public ShipSkinData[] shipSkins;
    public EnvironmentSkinData currentEnvironemntSkin { get; private set; }
    public ShipSkinData currentShipSkin { get; private set; }

    public void SetCurrentSkins()
    {
        foreach (EnvironmentSkinData environmentSkin in environmentSkins)
        {
            if (environmentSkin.id == DataManager.currentEnvironmentSkinId.GetValue())
            {
                currentEnvironemntSkin = environmentSkin;
                break;
            }
        }

        foreach (ShipSkinData shipSkin in shipSkins)
        {
            if (shipSkin.id == DataManager.currentShipSkinId.GetValue())
            {
                currentShipSkin = shipSkin;
                break;
            }
        }
    }
}
