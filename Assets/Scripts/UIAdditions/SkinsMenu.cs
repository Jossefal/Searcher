using UnityEngine;
using UnityEngine.UI;

public class SkinsMenu : MonoBehaviour
{
    public Image selectedSkinIcon;

    private SkinButton currentSkinBtn;

    private void Awake()
    {
        currentSkinBtn = GameObject.Find("SkinBtn" + PlayerPrefs.GetInt(Prefs.SKIN_INDEX_PREF, 0)).GetComponent<SkinButton>();
        selectedSkinIcon.sprite = currentSkinBtn.skinIcon;
        currentSkinBtn.Select();
    }

    public void ChangeSkin(SkinButton newSkinBtn)
    {
        PlayerPrefs.SetInt(Prefs.SKIN_INDEX_PREF, newSkinBtn.skinIndex);
        currentSkinBtn.Deselect();
        newSkinBtn.Select();
        selectedSkinIcon.sprite = newSkinBtn.skinIcon;
        currentSkinBtn = newSkinBtn;
    }
}
