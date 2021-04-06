using UnityEngine;

#pragma warning disable 649

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject[] heartImages;

    private void Start()
    {
        GameObject.FindWithTag("Ship").GetComponent<StatsController>().onHPChanged += DisplayHP;
    }

    private void DisplayHP(int hp)
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].SetActive(hp >= i + 1);
        }
    }
}
