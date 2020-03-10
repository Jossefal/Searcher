using UnityEngine;

[CreateAssetMenu(fileName = "NewSkinsContainer", menuName = "SkinsContainer")]
public class SkinsContainer : ScriptableObject
{
    public GameObject[] skins;

    public GameObject GetCurrentSkin()
    {
        return skins[PlayerPrefs.GetInt(Prefs.SKIN_INDEX_PREF, 0)];
    }
}
