using UnityEngine;

#pragma warning disable 649

public class RewardPanel : MonoBehaviour
{
    [SerializeField] private RectTransform lootPanelParent;

    [System.Serializable]
    public class LootPanelData
    {
        public LootType lootType;
        public LootPanel lootPanel;
        public Sprite icon;
        public string text;
    }

    [SerializeField] private LootPanelData[] lootPanelDatas;

    private LootPanel lootPanel;

    public void Open(LootData lootData)
    {
        gameObject.SetActive(true);

        LootPanelData lootPanelData = null;

        for (int i = 0; i < lootPanelDatas.Length; i++)
        {
            if (lootPanelDatas[i].lootType == lootData.lootType)
            {
                lootPanelData = lootPanelDatas[i];
                break;
            }
        }

        if (lootPanelData == null)
            return;

        if (lootPanel != null)
            Destroy(lootPanel.gameObject);

        lootPanel = Instantiate(lootPanelData.lootPanel, lootPanelParent.position, Quaternion.identity, lootPanelParent);
        lootPanel.icon = lootPanelData.icon;

        if (lootData.lootType == LootType.Diamonds || lootData.lootType == LootType.Lives)
        {
            lootPanel.text = Converter.ConvertToString(lootData.count);
        }
        else
            lootPanel.text = lootPanelData.text;
    }

    public void Close()
    {
        if (lootPanel != null)
            Destroy(lootPanel.gameObject);

        gameObject.SetActive(false);
    }
}
