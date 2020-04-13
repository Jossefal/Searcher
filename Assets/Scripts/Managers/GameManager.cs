using UnityEngine;

#pragma warning disable 649

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject ship;
    [SerializeField] private LayerMask obstacleLayers;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private DeathPanel deathPanel;
    [SerializeField] private InterfaceManager interfaceManager;

    public void RespawnShip()
    {
        Camera camera = Camera.main;
        Vector2 box = new Vector2(camera.orthographicSize * camera.aspect * 2f, camera.orthographicSize * 2f);
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(camera.transform.position, box, 0f, obstacleLayers);

        foreach (Collider2D item in collider2Ds)
        {
            Destroy(item.gameObject);
        }

        ship.SetActive(true);
        ship.transform.position = new Vector3(0f, ship.transform.position.y, 0f);
        ship.transform.rotation = new Quaternion(0, 0, 0, 0);

        interfaceManager.ShowObjects();
    }

    public void GameOver()
    {
        scoreManager.SendScore();
        interfaceManager.HideObjects();
        deathPanel.Open();
    }

    public void DestroyObject(GameObject destroyingObject)
    {
        Destroy(destroyingObject);
    }
}
