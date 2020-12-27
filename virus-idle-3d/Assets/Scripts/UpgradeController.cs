using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour
{
    GameObject tower;
    public int id;
    public float speed;

    private void Start()
    {
        tower = GameObject.FindGameObjectWithTag("Player");
        Destroy(gameObject, 20);
    }
    private void Update()
    {
        transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
    }

    private void OnMouseDown()
    {
        tower.GetComponent<TowerController>().Upgrade(id);
        Destroy(gameObject);
    }
}
