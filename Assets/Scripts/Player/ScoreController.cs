using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public ScoreManager scoreManager;

    private void Awake()
    {
        if(scoreManager == null)
            scoreManager = GameObject.FindWithTag("GManager").GetComponent<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        scoreManager.AddPoint();
        Destroy(coll.gameObject);
    }
}
