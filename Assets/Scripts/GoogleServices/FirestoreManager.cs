using System;
using System.Threading.Tasks;
using Firebase.Firestore;
using Firebase.Auth;
using Firebase.Extensions;

public static class FirestoreManager
{
    private static FirebaseFirestore db;
    private static FirebaseAuth auth;
    private static string leaderboardId = "Leaderboard";

    public static bool isInitialized { get; private set; }

    public static bool isAuthenticated
    {
        get
        {
            return auth != null && auth.CurrentUser != null;
        }
    }

    public static void Initialize()
    {
        if (isInitialized)
            return;

        db = FirebaseFirestore.DefaultInstance;

        UnityEngine.Debug.Log("Firebase - defaultInstance is " + (db == null ? "null" : "not null"));

        isInitialized = true;
    }

    public static void Auth(string authCode, Action<Task> callback)
    {
        auth = FirebaseAuth.DefaultInstance;

        UnityEngine.Debug.Log("Firebase - authCode is " + (authCode == "" ? "empty" : "not empty"));

        Credential credential = PlayGamesAuthProvider.GetCredential(authCode);

        UnityEngine.Debug.Log("Firebase - play games credential is " + (credential == null ? "null" : "not null"));

        auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
                UnityEngine.Debug.LogError("Firebase - SignInWithCredentialAsync was canceled!");
            else if (task.IsFaulted)
                UnityEngine.Debug.LogError("Firebase - SignInWithCredentialAsync was failed with error: " + task.Exception);
            else
                UnityEngine.Debug.Log("Firebase - SignInWithCredentialAsync was completed!");

            callback?.Invoke(task);
        });
    }

    public static void LoadLeaderboardData(int limit, Action<Task, LeaderboardData> callback)
    {
        CollectionReference collectionRef = db.Collection(leaderboardId);

        UnityEngine.Debug.Log("Firebase - start load scores from leaderboard");

        Query query = collectionRef.OrderByDescending(UserScoreData.USER_SCORE_PROPERTY_NAME).Limit(limit);

        query.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (!task.IsCompleted)
                UnityEngine.Debug.Log("Firebase - query.GetSnapshotAsync was completed!");
            else
            {
                if (task.IsCanceled)
                    UnityEngine.Debug.LogError("Firebase - query.GetSnapshotAsync was canceled with error - " + task.Exception);
                else if (task.IsFaulted)
                    UnityEngine.Debug.LogError("Firebase - query.GetSnapshotAsync was faulted with error - " + task.Exception);

                callback?.Invoke(task, null);
                return;
            }

            QuerySnapshot querySnapshot = task.Result;
            LeaderboardData leaderboardData = new LeaderboardData();

            foreach (DocumentSnapshot docSnapshot in querySnapshot.Documents)
            {
                UserScoreData userScoreData = docSnapshot.ConvertTo<UserScoreData>();

                leaderboardData.scores.Add(userScoreData);
            }

            callback?.Invoke(task, leaderboardData);
        });
    }

    public static void SendScore(UserScoreData userScoreData, Action<Task> callback)
    {
        if (!isAuthenticated || userScoreData == null)
            return;

        UnityEngine.Debug.Log("Firebase - start load userDocRef");

        DocumentReference userDocRef = db.Collection(leaderboardId).Document(auth.CurrentUser.UserId);

        userDocRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (!task.IsCompleted)
                UnityEngine.Debug.Log("Firebase - userDocRef.GetSnapshotAsync was completed!");
            else
            {
                if (task.IsCanceled)
                    UnityEngine.Debug.Log("Firebase - userDocRef.GetSnapshotAsync was canceled!");
                else if (task.IsFaulted)
                    UnityEngine.Debug.Log("Firebase - userDocRef.GetSnapshotAsync was faulted!");

                callback?.Invoke(task);
                return;
            }

            if (task.Result.Exists)
            {
                UserScoreData savedScoreData = task.Result.ConvertTo<UserScoreData>();

                if (userScoreData.userScore <= savedScoreData.userScore)
                {
                    callback?.Invoke(task);
                    return;
                }
            }

            UnityEngine.Debug.Log("Firebase - start write userDocRef");

            userDocRef.SetAsync(userScoreData).ContinueWithOnMainThread(secondTask =>
            {
                if (secondTask.IsCanceled)
                    UnityEngine.Debug.Log("Firebase - userDocRef.SetAsync was canceled!");
                else if (secondTask.IsFaulted)
                    UnityEngine.Debug.Log("Firebase - userDocRef.SetAsync was faulted!");
                else
                    UnityEngine.Debug.Log("Firebase - userDocRef.SetAsync was completed!");

                callback?.Invoke(secondTask);
            });
        });
    }
}
