using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;

public static class GPGSManager
{
    public static bool isAuthenticated
    {
        get
        {
            if (PlayGamesPlatform.Instance != null) return PlayGamesPlatform.Instance.IsAuthenticated();
            return false;
        }
    }

    public static bool isFirstAuth
    {
        get
        {
            return !PlayerPrefs.HasKey(FIRST_GPG_AUTH_CHECK_PREF);
        }
    }

    public const string SAVE_FILE_NAME = "Save";
    public const string LEADER_BOARD_ID = "CgkIvcrMjfQeEAIQAQ";

    public const string FIRST_GPG_AUTH_CHECK_PREF = "FIRST_GPG_AUTH_CHECK";

    private static ISavedGameClient savedGameClient;
    private static ISavedGameMetadata currentSavedGameMetadata;

    public static void Initialize(bool debug)
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = debug;
        PlayGamesPlatform.Activate();
    }

    public static void Auth(Action<bool> onAuth)
    {
        Social.localUser.Authenticate((success =>
        {
            if (success)
                savedGameClient = PlayGamesPlatform.Instance.SavedGame;

            onAuth(success);

            if (success)
                PlayerPrefs.SetInt(FIRST_GPG_AUTH_CHECK_PREF, 1);
        }));
    }

    public static void OpenSaveData()
    {
        OpenSaveData(SAVE_FILE_NAME, (status, metadata) => currentSavedGameMetadata = metadata);
    }

    private static void OpenSaveData(string fileName, Action<SavedGameRequestStatus, ISavedGameMetadata> onDataOpen)
    {
        if (!isAuthenticated)
        {
            onDataOpen(SavedGameRequestStatus.AuthenticationError, null);
            return;
        }

        savedGameClient.OpenWithAutomaticConflictResolution(fileName, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLastKnownGood, onDataOpen);
    }

    public static void ReadSaveData(string fileName, Action<SavedGameRequestStatus, byte[]> onDataRead)
    {
        if (!isAuthenticated)
        {
            onDataRead(SavedGameRequestStatus.AuthenticationError, null);
            return;
        }

        OpenSaveData(fileName, (status, metadata) =>
        {
            if (status == SavedGameRequestStatus.Success)
            {
                savedGameClient.ReadBinaryData(metadata, onDataRead);
            }
        });
    }

    public static void WriteSaveData(string fileName, byte[] data, Action<bool> callback)
    {
        if (!isAuthenticated || data == null || data.Length == 0)
            return;

        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        savedGameClient.CommitUpdate(currentSavedGameMetadata, updatedMetadata, data, (status, metadata) =>
        {
            Debug.Log("Commit saved game request status: " + status);
            if (status == SavedGameRequestStatus.Success)
                callback?.Invoke(true);
            else
                callback?.Invoke(false);
        });
    }

    public static void ReportScore(int score)
    {
        PlayGamesPlatform.Instance.ReportScore(score, LEADER_BOARD_ID, null);
    }

    public static void ShowLeaderBoardUI()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(LEADER_BOARD_ID);
    }

    public static void LoadLeaderboardData(TimeScope timeScope, int maxCount, Action<LeaderboardData> onDataLoaded)
    {
        if (!isAuthenticated)
        {
            onDataLoaded(null);
            return;
        }

        ILeaderboard lb = PlayGamesPlatform.Instance.CreateLeaderboard();
        lb.id = LEADER_BOARD_ID;
        lb.timeScope = timeScope;
        lb.range = new Range(1, maxCount);

        lb.LoadScores(ok =>
        {
            if (ok)
            {
                string[] userIds = new string[lb.scores.Length];

                for (int i = 0; i < lb.scores.Length; i++)
                {
                    userIds[i] = lb.scores[i].userID;
                }

                Social.LoadUsers(userIds, (users) =>
                {
                    IUserProfile FindUser(string userID)
                    {
                        for (int i = 0; i < users.Length; i++)
                        {
                            if (users[i].id == userID)
                                return users[i];
                        }
                        return null;
                    }

                    LeaderboardData leaderboardData = new LeaderboardData();
                    leaderboardData.players = new LeaderboardData.PlayerScoreData[lb.scores.Length];

                    for (int i = 0; i < lb.scores.Length; i++)
                    {
                        IUserProfile user = FindUser(lb.scores[i].userID);
                        leaderboardData.players[i].userName = user.userName;
                        leaderboardData.players[i].score = lb.scores[i].formattedValue;
                    }

                    onDataLoaded(leaderboardData);
                });
            }
            else
                onDataLoaded(null);
        });
    }
}