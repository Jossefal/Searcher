using System.Collections.Generic;
using UnityEngine;

public class DetectionTrigger : MonoBehaviour
{
    private List<GameObject> objects = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D coll)
    {
        objects.Add(coll.gameObject);
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        objects.Remove(coll.gameObject);
    }

    public GameObject GetNearestObject()
    {
        if (objects.Count == 0)
        {
            return null;
        }
        GameObject nearestObject = objects[0];
        for (int i = 1; i < objects.Count; i++)
        {
            if (Vector3.Distance(transform.position, objects[i].transform.position) < Vector3.Distance(transform.position, nearestObject.transform.position))
            {
                nearestObject = objects[i];
            }
        }
        return nearestObject;
    }
}
