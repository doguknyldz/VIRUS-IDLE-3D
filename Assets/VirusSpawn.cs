using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusSpawn : MonoBehaviour
{
    public GameObject virusPrefab;
    public float spawnTime;
    private void Start()
    {
        StartCoroutine(Spawn());
    }


    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnTime);
        GameObject virus = Instantiate(virusPrefab);
        virus.transform.position = new Vector3(10, Random.Range(0f, 2f), Random.Range(-1.5f, 1.5f));
        StartCoroutine(Spawn());
    }
}
