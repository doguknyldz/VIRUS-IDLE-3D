using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCS : MonoBehaviour
{
    public GameObject target;
    public float damage;

    private void Update()
    {
        transform.position = Vector3.Slerp(transform.position, target.transform.position, 0.05f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.tag);
        if (collision.collider.tag == "Enemy")
        {
            collision.gameObject.GetComponent<VirusCS>().health -= damage;
            Destroy(gameObject);
        }
    }
}
