using Firebase.Firestore;

[FirestoreData]
public class UserScoreData
{
    [FirestoreProperty("UserName")] public string userName { get; set; }
    [FirestoreProperty("UserScore")] public int userScore { get; set; }
    public const string USER_SCORE_PROPERTY_NAME = "UserScore";

    public UserScoreData()
    {
        this.userName = "Player";
        this.userScore = 1;
    }

    public UserScoreData(string userName, int userScore)
    {
        this.userName = userName;
        this.userScore = userScore;
    }
}
