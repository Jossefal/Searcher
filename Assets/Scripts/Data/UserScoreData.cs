using Firebase.Firestore;

[FirestoreData]
public class UserScoreData
{
    [FirestoreProperty("UserName")] public string userName { get; set; }
    [FirestoreProperty("UserScore")] public int userScore { get; set; }
    public const string USER_SCORE_PROPERTY_NAME = "UserScore";

    public UserScoreData(string userName, int userScore)
    {
        this.userName = userName;
        this.userScore = userScore;
    }
}
