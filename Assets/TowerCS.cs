using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCS : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject turret;
    public float cooldown;
    public float damage;
    bool isShot=true;
    private void Start()
    {

    }

    private void Update()
    {
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            Shot(collision.collider.transform.position);
        }
    }

    private void Shot(Vector3 pos)
    {
        if (isShot)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = Vector3.Lerp(turret.transform.position, pos, 1);
            isShot = false;
        }
        
    }
}
