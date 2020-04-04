using UnityEngine;

[CreateAssetMenu(fileName = "NewSkinsContainer", menuName = "SkinsContainer")]
public class SkinsContainer : ScriptableObject
{
    public Sprite[] skins;

    public Sprite GetCurrentSkin()
    {
        return skins[PlayerPrefs.GetInt(Prefs.SKIN_INDEX_PREF, 0)];
    }

    public Sprite GetSkin(int index)
    {
        return skins[index];
    }
}
