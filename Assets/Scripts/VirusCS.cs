using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirusCS : MonoBehaviour
{
    GameObject tower;
    Canvas canvas;
    public GameObject coinTextPrefab;
    public float speed;
    public float health;
    public int coin;

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        tower = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);

        if (health<=0)
        {
            GameObject coinText = Instantiate(coinTextPrefab, canvas.transform);
            coinText.transform.position = new Vector3(Camera.main.WorldToScreenPoint(transform.position).x,
                Camera.main.WorldToScreenPoint(transform.position).y,0);
            coinText.GetComponent<Text>().text = "+" + coin;
            tower.GetComponent<TowerCS>().totalCoin += coin;
            Destroy(coinText, 2);
            Destroy(gameObject);
        }
    }

}
