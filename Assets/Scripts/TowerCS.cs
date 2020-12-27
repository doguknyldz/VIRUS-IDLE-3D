using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCS : MonoBehaviour
{
    public GameObject turret;
    public GameObject bulletPrefab;
    public float cooldown=1;
    public float damage=10;
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
            if (isShot)
            {
                GameObject bullet = Instantiate(bulletPrefab);
                bullet.transform.position = turret.transform.position;
                bullet.GetComponent<BulletCS>().target = collision.gameObject;
                bullet.GetComponent<BulletCS>().damage = damage;
                isShot = false;
                StartCoroutine(Time());
            }
        }
    }

    private void Shot(Vector3 pos)
    {
    }

    IEnumerator Time()
    {
        yield return new WaitForSeconds(cooldown);
        isShot = true;
    }
}
