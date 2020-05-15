using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

#pragma warning disable 649

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private Text statusText;
    [SerializeField] private TimeScope timeScope;
    [SerializeField] private int maxCount = 10;
    [SerializeField] private GameObject playerScoresContainer;
    [SerializeField] private PlayerScorePanel[] playersScorePanels;

    private bool isLoading;

    private void OnEnable()
    {
        Load();
    }

    public void Load()
    {
        if (isLoading)
            return;

        playerScoresContainer.SetActive(false);
        statusText.gameObject.SetActive(true);

        if(!GPGSManager.isAuthenticated)
        {
            statusText.text = "To view the leaderboard, Google Play Games authorization is required";
            return;
        }

        isLoading = true;
        statusText.text = "Loading...";
        

        GPGSManager.LoadLeaderboardData(timeScope, maxCount, (leaderboardData) =>
        {
            if (leaderboardData == null)
            {
                statusText.gameObject.SetActive(false);
                return;
            }

            for (int i = 0; i < playersScorePanels.Length; i++)
            {
                if (i < leaderboardData.players.Length)
                {
                    playersScorePanels[i].score = leaderboardData.players[i].score;
                    playersScorePanels[i].playerName = (i + 1) + " " + leaderboardData.players[i].userName;
                    playersScorePanels[i].gameObject.SetActive(true);
                }
                else
                    playersScorePanels[i].gameObject.SetActive(false);
            }

            isLoading = false;
            statusText.gameObject.SetActive(false);
            playerScoresContainer.SetActive(true);
        });
    }
}