using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCS : MonoBehaviour
{
    public GameObject target;
    public float damage;
    public int coin;

    private void Update()
    {
        transform.position = Vector3.Slerp(transform.position, target.transform.position, 0.2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            collision.gameObject.GetComponent<VirusCS>().health -= damage;
            collision.gameObject.GetComponent<VirusCS>().coin = coin;
            Destroy(gameObject);
        }
    }
}
