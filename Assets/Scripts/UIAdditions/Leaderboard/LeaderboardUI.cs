using UnityEngine;
using UnityEngine.SocialPlatforms;

#pragma warning disable 649

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private GameObject loadPanel;
    [SerializeField] private TimeScope timeScope;
    [SerializeField] private PlayerScorePanel[] playersScorePanels;

    public void Open()
    {
        loadPanel.SetActive(true);

        GPGSManager.LoadLeaderboardData(timeScope, (leaderboardData) =>
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

            loadPanel.SetActive(false);
        });
    }
}