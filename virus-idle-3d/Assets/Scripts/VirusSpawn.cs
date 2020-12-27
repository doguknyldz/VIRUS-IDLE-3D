using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusSpawn : MonoBehaviour
{
    public GameObject virusPrefab;
    public GameObject[] UpgradesPrefab;
    public float spawnTime;
    private void Start()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnUpgrade());
    }


    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(spawnTime);
        GameObject virus = Instantiate(virusPrefab);
        virus.transform.position = new Vector3(13, Random.Range(0f, 2f), Random.Range(-1.5f, 1.5f));
        StartCoroutine(SpawnEnemy());
    }
    IEnumerator SpawnUpgrade()
    {
        yield return new WaitForSeconds(5);
        GameObject Upgrade = Instantiate(UpgradesPrefab[Random.Range(0,UpgradesPrefab.Length)]);
        Upgrade.transform.position = new Vector3(13, Random.Range(0f, 2f), Random.Range(-1.5f, 1.5f));
        StartCoroutine(SpawnUpgrade());
    }
}
