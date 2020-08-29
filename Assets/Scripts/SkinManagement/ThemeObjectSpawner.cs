using UnityEngine;

#pragma warning disable 649

public class ThemeObjectSpawner : MonoBehaviour
{
    [System.Serializable]
    public class ThemeObject
    {
        public int themeId;
        public GameObject gameObject;
    }

    [SerializeField] private ThemeObject[] themeObjects;

    private void Awake()
    {
        foreach (ThemeObject themeObject in themeObjects)
        {
            if (themeObject.themeId == DataManager.currentThemeId.GetValue())
            {
                if (themeObject.gameObject != null)
                    Instantiate(themeObject.gameObject, transform.position, Quaternion.identity, transform);

                Destroy(this);
                return;
            }
        }
    }
}
