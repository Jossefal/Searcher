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
            if (PlayGamesPlatform.Instance != null) return PlayGamesPlatform.Instance.IsAuthenticated();
            return false;
        }
    }

    public const string SAVE_FILE_NAME = "Save";

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
            if (success) savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            onAuth(success);
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

    public static void WriteSaveData(string fileName, byte[] data)
    {
        if (!isAuthenticated || data == null || data.Length == 0)
            return;

        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        savedGameClient.CommitUpdate(currentSavedGameMetadata, updatedMetadata, data, (status, metadata) => Debug.Log("Commit saved game request status: " + status));
    }
}