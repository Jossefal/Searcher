using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 649

public class PickableManager : MonoBehaviour
{
    [System.Serializable]
    public class Range
    {
        public int min;
        public int max;
    }

    [System.Serializable]
    public class PickableSpawnData
    {
        private int counter;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Range range;

        public GameObject prefab
        {
            get
            {
                return _prefab;
            }
        }

        public void ProccesCounter()
        {
            counter--;
        }

        public bool CheckCounter()
        {
            return counter <= 0;
        }

        public void RestartCounter()
        {
            counter = Random.Range(range.min, range.max + 1);
        }
    }

    [SerializeField] private PickableSpawnData[] pickables;

    private Queue<GameObject> spawnQueue = new Queue<GameObject>();

    public void Initialize()
    {
        for (int i = 0; i < pickables.Length; i++)
        {
            pickables[i].RestartCounter();
        }
    }

    public void ProccesCounters()
    {
        for (int i = 0; i < pickables.Length; i++)
        {
            if (pickables[i].CheckCounter())
            {
                pickables[i].RestartCounter();
                spawnQueue.Enqueue(pickables[i].prefab);
            }
        }
    }

    public bool QueryNotEmpty()
    {
        return spawnQueue.Count > 0;
    }

    public GameObject GetPickable()
    {
        return spawnQueue.Dequeue();
    }
}
