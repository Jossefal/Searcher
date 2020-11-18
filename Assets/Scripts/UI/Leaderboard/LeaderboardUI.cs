using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 649

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private Text statusText;
    [SerializeField] private int limit = 10;
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

        if (!FirestoreManager.isInitialized)
        {
            statusText.text = "Failed to load leaderboard";
            return;
        }

        isLoading = true;
        statusText.text = "Loading...";

        if (FirestoreManager.isAuthenticated)
        {
            FirestoreManager.SendRecord(task =>
            {
                LoadLeaderboard();
            });
        }
        else
            LoadLeaderboard();
    }

    private void LoadLeaderboard()
    {
        FirestoreManager.LoadLeaderboardData(limit, (task, leaderboardData) =>
        {
            if (!task.IsCompleted)
            {
                statusText.text = "Failed to load leaderboard";
                return;
            }

            for (int i = 0; i < playersScorePanels.Length; i++)
            {
                if (i < leaderboardData.scores.Count)
                {
                    playersScorePanels[i].score = Converter.ConvertToString(leaderboardData.scores[i].userScore);
                    playersScorePanels[i].playerName = (i + 1) + ". " + leaderboardData.scores[i].userName;
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