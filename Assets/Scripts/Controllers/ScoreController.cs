using UnityEngine;

#pragma warning disable 649

public class ScoreController : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;

    private void Awake()
    {
        if (scoreManager == null)
            scoreManager = GameObject.FindWithTag("GManager").GetComponent<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        scoreManager.AddPoint();
        Destroy(coll.gameObject);
    }
}
