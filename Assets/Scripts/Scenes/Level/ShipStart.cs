using UnityEngine;

#pragma warning disable 649

public class ShipStart : MonoBehaviour
{
    [SerializeField] private Transform ship;
    [SerializeField] private Vector3 finalPos;
    [SerializeField] private float speed;
    [SerializeField] private MovingController movingController;
    [SerializeField] private FollowingController cameraController;
    [SerializeField] private GameObject[] hiddenObjects;

    private void Awake()
    {
        movingController.enabled = false;
        cameraController.enabled = false;

        foreach (GameObject obj in hiddenObjects)
        {
            obj.SetActive(false);
        }
    }

    private void Update()
    {
        if (ship.position != finalPos)
            ship.position = Vector3.MoveTowards(ship.position, finalPos, speed * Time.deltaTime);
        else
        {
            movingController.enabled = true;
            cameraController.enabled = true;

            foreach (GameObject obj in hiddenObjects)
            {
                obj.SetActive(true);
            }

            Destroy(gameObject);
        }
    }
}
