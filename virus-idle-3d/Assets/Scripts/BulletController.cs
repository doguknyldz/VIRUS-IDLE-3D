using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Vector3 target;
    public float damage;
    public int coin;
    VirusController vc;
    private void Update()
    {
        transform.position = Vector3.Slerp(transform.position, target, 0.2f);
        if (transform.position == target)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            vc = collision.gameObject.GetComponent<VirusController>();
            vc.health -= damage;
            vc.coin = coin;
            Destroy(gameObject);
        }
    }
}
