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

        if (!FirestoreManager.isAuthenticated)
        {
            statusText.text = "Trouble to loading leaderboard";
            return;
        }

        isLoading = true;
        statusText.text = "Loading...";


        FirestoreManager.LoadLeaderboardData(limit, (task, leaderboardData) =>
        {
            if (!task.IsCompleted)
            {
                statusText.gameObject.SetActive(false);
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