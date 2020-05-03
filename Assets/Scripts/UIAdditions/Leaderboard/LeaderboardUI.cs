using UnityEngine;
using UnityEngine.SocialPlatforms;

#pragma warning disable 649

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private GameObject loadPanel;
    [SerializeField] private TimeScope timeScope;
    [SerializeField] private int maxCount = 10;
    [SerializeField] private PlayerScorePanel[] playersScorePanels;

    private bool isLoading;

    private void OnEnable()
    {
        Load();
    }

    public void Load()
    {
        if(isLoading)
            return;

        isLoading = true;
        loadPanel.SetActive(true);

        GPGSManager.LoadLeaderboardData(timeScope, maxCount, (leaderboardData) =>
        {
            if(leaderboardData == null)
            {
                loadPanel.SetActive(false);
                return;
            }

            for(int i = 0; i < playersScorePanels.Length; i++)
            {
                if(i < leaderboardData.players.Length)
                {
                    playersScorePanels[i].score = (i + 1) + ". " + leaderboardData.players[i].userName + ": " + leaderboardData.players[i].score;
                    playersScorePanels[i].gameObject.SetActive(true);
                }
                else
                    playersScorePanels[i].gameObject.SetActive(false);
            }

            isLoading = false;
            loadPanel.SetActive(false);
        });
    }
}