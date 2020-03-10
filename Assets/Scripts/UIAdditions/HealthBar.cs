using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject[] hearts;
    public StatsController statsController;

    public void DisplayHealth()
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            if(statsController.GetCurrentHp() >= i + 1)
                hearts[i].SetActive(true);
            else
                hearts[i].SetActive(false);
        }
    }
}
