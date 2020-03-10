using UnityEngine;

public class ShipController : MonoBehaviour
{
    public GameObject[] hiddenObjects;
    public ScoreManager scoreManager;
    public DeathPanel deathPanel;

    private void Awake()
    {
        GameObject.FindWithTag("MainCamera").GetComponent<FollowingController>().pursuedObject = transform;
    }

    public void OnDeath()
    {
        Destroy(gameObject);
    }

    public void OnGameOver()
    {
        scoreManager.SendScore();
        deathPanel.Open();
        HideObjects();
    }

    private void HideObjects()
    {
        for(int i = 0; i < hiddenObjects.Length; i++)
        {
            hiddenObjects[i].SetActive(false);
        }
    }

    private void ShowObjects()
    {
        for(int i = 0; i < hiddenObjects.Length; i ++)
        {
            hiddenObjects[i].SetActive(true);
        }
    }
}
