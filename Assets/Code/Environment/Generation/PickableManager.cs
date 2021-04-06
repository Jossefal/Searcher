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

        public bool CheckCounter()
        {
            return counter-- <= 0;
        }

        public void RestartCounter()
        {
            counter = Random.Range(range.min, range.max + 1);
        }
    }

    [SerializeField] private PickableSpawnData[] pickables;

    public void Initialize()
    {
        for (int i = 0; i < pickables.Length; i++)
        {
            pickables[i].RestartCounter();
        }
    }

    public void Evaluate(Area area)
    {
        bool objectIsSpawned = false;

        for (int i = 0; i < pickables.Length; i++)
        {
            if (pickables[i].CheckCounter() && !objectIsSpawned)
            {
                area.SpawnObject(pickables[i].prefab);
                objectIsSpawned = true;
                pickables[i].RestartCounter();
            }
        }
    }
}
