using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusCS : MonoBehaviour
{
    public float speed;
    public float health;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
    }

}
