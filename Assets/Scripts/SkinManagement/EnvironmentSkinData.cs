using UnityEngine;

#pragma warning disable 649

[CreateAssetMenu(fileName = "NewEnvironmentSkinData", menuName = "ScriptableObject`s/EnvironmentSkinData")]
public class EnvironmentSkinData : SkinData
{
    public GameObject background;
    public GameObject meteorSpite;
    public GameObject blackHoleSprite;
}
