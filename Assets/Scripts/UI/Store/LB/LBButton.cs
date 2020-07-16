using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LBButton : MonoBehaviour
{
    [SerializeField] protected LootBox lootBox;
    [SerializeField] protected RewardPanel rewardPanel;

    protected Button button;

    protected virtual void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OpenLootBox);
        Initialize();
    }

    protected virtual void Initialize()
    {
        //TODO
    }

    protected virtual void OpenLootBox()
    {
        LootData lootData = lootBox.GetLoot();

        switch (lootData.lootType)
        {
            case LootType.Diamonds:
                DataManager.diamondsCount += lootData.count;
                break;
            case LootType.Lives:
                DataManager.livesCount += lootData.count;
                break;
        }

        rewardPanel.Open(lootData);

        OnOpen();
    }

    protected virtual void OnOpen()
    {
        //TODO
    }
}
