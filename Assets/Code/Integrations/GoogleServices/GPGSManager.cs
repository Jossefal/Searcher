using System;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;

public static class GPGSManager
{
    public static bool isAuthenticated
    {
        get
        {
            return PlayGamesPlatform.Instance != null && PlayGamesPlatform.Instance.IsAuthenticated();
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
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().RequestServerAuthCode(false).Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = debug;
        // PlayGamesPlatform.Activate();
    }

    public static void Auth(Action<bool> onAuth)
    {
        PlayGamesPlatform.Instance.Authenticate((success =>
        {
            Debug.Log("PlayGamesAuth - succes==" + success);

            if (success)
            {
                savedGameClient = PlayGamesPlatform.Instance.SavedGame;
                OpenSaveData();
            }

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

        if (currentSavedGameMetadata != null && currentSavedGameMetadata.IsOpen)
        {
            Debug.Log("GPGSManager - start commit update saved game");

            SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
            SavedGameMetadataUpdate updatedMetadata = builder.Build();
            savedGameClient.CommitUpdate(currentSavedGameMetadata, updatedMetadata, data, (commitStatus, newMetadata) =>
            {
                Debug.Log("GPGSManager - commit saved game request status: " + commitStatus);
                if (commitStatus == SavedGameRequestStatus.Success)
                    callback?.Invoke(true);
                else
                    callback?.Invoke(false);

                OpenSaveData();
            });
        }
        else
        {
            Debug.Log("GPGSManager - start open savedGameMetadata");

            OpenSaveData(fileName, (openStatus, metadata) =>
            {
                if (openStatus == SavedGameRequestStatus.Success)
                {
                    Debug.Log("GPGSManager - start commit update saved game");

                    SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
                    SavedGameMetadataUpdate updatedMetadata = builder.Build();
                    savedGameClient.CommitUpdate(metadata, updatedMetadata, data, (commitStatus, newMetadata) =>
                    {
                        Debug.Log("GPGSManager - commit saved game request status: " + commitStatus);
                        if (commitStatus == SavedGameRequestStatus.Success)
                            callback?.Invoke(true);
                        else
                            callback?.Invoke(false);

                        OpenSaveData();
                    });
                }
                else
                    Debug.Log("GPGSManager - open savedGameMetada is failed");
            });
        }
    }

    public static void ReportScore(int score)
    {
        PlayGamesPlatform.Instance.ReportScore(score, LEADER_BOARD_ID, null);
    }

    public static void ShowLeaderBoardUI()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(LEADER_BOARD_ID);
    }

    public static string GetServerAuthCode()
    {
        if (isAuthenticated)
            return PlayGamesPlatform.Instance.GetServerAuthCode();
        else
            return "";
    }

    public static string GetUserName()
    {
        if (isAuthenticated)
            return PlayGamesPlatform.Instance.GetUserDisplayName();
        else
            return "";
    }
}