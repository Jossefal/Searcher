using UnityEngine;

#pragma warning disable 649

public class ThemeSkinChanger : MonoBehaviour
{
    [System.Serializable]
    public class ThemeSkin
    {
        public int themeId;
        public Sprite sprite;
    }

    [SerializeField] private ThemeSkin[] themeSkins;

    private void Awake()
    {
        foreach (ThemeSkin themeSkin in themeSkins)
        {
            if (themeSkin.themeId == DataManager.currentThemeId.GetValue())
            {
                GetComponent<SpriteRenderer>().sprite = themeSkin.sprite;

                Destroy(this);
                return;
            }
        }
    }
}
