using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusSpawn : MonoBehaviour
{
    public GameObject tower;
    public TowerController tc;
    public Canvas canvas;
    public GameObject virusPrefab;
    public GameObject[] UpgradesPrefab;
    public Transform[] SpawnPoints;
    public float spawnTimeEnemy;
    public float spawnTimeUpgrade;
    float timeEnemy;
    float timeUpgrade;

    private void Update()
    {
        timeEnemy += Time.deltaTime;
        timeUpgrade += Time.deltaTime;
        if (timeEnemy >= spawnTimeEnemy)
        {
            SpawnEnemy();
            timeEnemy = 0;
        }
        if (timeUpgrade >= spawnTimeUpgrade)
        {
            SpawnUpgrade();
            timeUpgrade = 0;
        }
    }

    void SpawnEnemy()
    {
        GameObject virus = Instantiate(virusPrefab);
        virus.transform.position = SpawnPoints[Random.Range(0, SpawnPoints.Length)].position;
        virus.GetComponent<VirusController>().tower = tower;
        virus.GetComponent<VirusController>().canvas = canvas;
        tc.VirusAdd(virus);
    }
    void SpawnUpgrade()
    {
        GameObject Upgrade = Instantiate(UpgradesPrefab[Random.Range(0, UpgradesPrefab.Length)]);
        Upgrade.transform.position = SpawnPoints[Random.Range(0, SpawnPoints.Length)].position;
    }
}
