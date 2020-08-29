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

        isInitialized = true;
    }

    public static void Auth(string authCode, Action<Task> callback)
    {
        auth = FirebaseAuth.DefaultInstance;

        Credential credential = PlayGamesAuthProvider.GetCredential(authCode);

        auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
                Console.WriteLine("SignInWithCredentialAsync was canceled!");
            else if (task.IsFaulted)
                Console.WriteLine("SignInWithCredentialAsync was faulted!");
            else
                Console.WriteLine("SignInWithCredentialAsync was completed!");

            callback?.Invoke(task);
        });
    }

    public static void LoadLeaderboardData(int limit, Action<Task, LeaderboardData> callback)
    {
        CollectionReference collectionRef = db.Collection(leaderboardId);

        Query query = collectionRef.OrderByDescending(UserScoreData.USER_SCORE_PROPERTY_NAME).Limit(limit);

        query.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (!task.IsCompleted)
                Console.WriteLine("query.GetSnapshotAsync was completed!");
            else
            {
                if (task.IsCanceled)
                    Console.WriteLine("query.GetSnapshotAsync was canceled!");
                else if (task.IsFaulted)
                    Console.WriteLine("query.GetSnapshotAsync was faulted!");

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

        DocumentReference userDocRef = db.Collection(leaderboardId).Document(auth.CurrentUser.UserId);

        userDocRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (!task.IsCompleted)
                Console.WriteLine("userDocRef.GetSnapshotAsync was completed!");
            else
            {
                if (task.IsCanceled)
                    Console.WriteLine("userDocRef.GetSnapshotAsync was canceled!");
                else if (task.IsFaulted)
                    Console.WriteLine("userDocRef.GetSnapshotAsync was faulted!");

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

            userDocRef.SetAsync(userScoreData).ContinueWithOnMainThread(secondTask =>
            {
                if (secondTask.IsCanceled)
                    Console.WriteLine("userDocRef.SetAsync was canceled!");
                else if (secondTask.IsFaulted)
                    Console.WriteLine("userDocRef.SetAsync was faulted!");
                else
                    Console.WriteLine("userDocRef.SetAsync was completed!");

                callback?.Invoke(secondTask);
            });
        });
    }
}
