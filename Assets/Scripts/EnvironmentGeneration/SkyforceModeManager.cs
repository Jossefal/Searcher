using System.Collections;
using UnityEngine;

#pragma warning disable 649

public class SkyforceModeManager : MonoBehaviour
{
    public int difficultyLevel
    {
        get
        {
            return (int)difficultyCurve.Evaluate(activationsCount);
        }
    }

    private AreasManager areasManager;

    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private float inactiveTime;
    [SerializeField] private float activeTime;
    [SerializeField] private AnimationCurve difficultyCurve;
    [SerializeField] private GameObject shipGun;

    private int activationsCount;

    private void Awake()
    {
        areasManager = GetComponent<AreasManager>();

        StartCoroutine(Inactive());
    }

    private IEnumerator Inactive()
    {
        areasManager.isSkyforceMode = false;

        if (shipGun.activeInHierarchy)
            Invoke("SetActiveShipGun", 3.5f);

        yield return new WaitForSeconds(inactiveTime);

        StartCoroutine(Active());
    }

    private IEnumerator Active()
    {
        areasManager.isSkyforceMode = true;
        activationsCount++;
        enemySpawner.Activate(activeTime);

        if (!shipGun.activeInHierarchy)
            Invoke("SetActiveShipGun", 3.5f);

        yield return new WaitForSeconds(activeTime);

        StartCoroutine(Inactive());
    }

    private void SetActiveShipGun()
    {
        shipGun.SetActive(!shipGun.activeInHierarchy);
    }
}
