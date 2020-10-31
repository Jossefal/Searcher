using UnityEngine;

public class LivesBuyBtn : MonoBehaviour
{
    [SerializeField] private int livesCount;
    [SerializeField] private int diamondsPrice;
    [SerializeField] private GameObject rewardPanel;
    [SerializeField] private GameObject shopOpener;

    private void Buy()
    {
        if (DataManager.CheckValueCount(ValueVariant.DiamondsCount, diamondsPrice))
        {
            DataManager.diamondsCount -= diamondsPrice;
            DataManager.livesCount -= livesCount;
            rewardPanel.SetActive(true);
        }
        else
            shopOpener.SetActive(true);
    }
}
