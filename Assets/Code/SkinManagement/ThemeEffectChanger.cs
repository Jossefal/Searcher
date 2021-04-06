using UnityEngine;

#pragma warning disable 649

public class ThemeEffectChanger : MonoBehaviour
{
    [System.Serializable]
    public class ThemeEffect
    {
        public int themeId;
        public GameObject prefab;
    }

    [SerializeField] private ThemeEffect[] deathEffects;

    private void Awake()
    {
        foreach (ThemeEffect deathEffect in deathEffects)
        {
            if (deathEffect.themeId == DataManager.currentThemeId.GetValue())
            {
                GetComponent<EffectsController>().deathEffectPrefab = deathEffect.prefab;

                Destroy(this);
                return;
            }
        }
    }
}
