using System.Collections;
using UnityEngine;

#pragma warning disable 649

public class SkyforceModeManager : MonoBehaviour
{
    private AreasManager areasManager;

    [SerializeField] private GameObject enemySpawner;
    [SerializeField] private float inactiveTime;
    [SerializeField] private float activeTime;
    [SerializeField] private GameObject shipGun;

    private void Awake()
    {
        areasManager = GetComponent<AreasManager>();

        StartCoroutine(Inactive());
    }

    private IEnumerator Inactive()
    {
        areasManager.isSkyforceMode = false;
        enemySpawner.SetActive(false);

        if (shipGun.activeInHierarchy)
            Invoke("SetActiveShipGun", 3.5f);

        yield return new WaitForSeconds(inactiveTime);

        StartCoroutine(Active());
    }

    private IEnumerator Active()
    {
        areasManager.isSkyforceMode = true;
        enemySpawner.SetActive(true);

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
