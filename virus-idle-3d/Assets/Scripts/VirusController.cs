using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirusController : MonoBehaviour
{
    GameObject tower;
    public Slider healthBarPrefab;
    Slider healthBar;
    Canvas canvas;
    public GameObject coinTextPrefab;
    public float speed;
    public float health;
    public int coin;

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        tower = GameObject.FindGameObjectWithTag("Player");
        healthBar = Instantiate(healthBarPrefab, canvas.transform);
        healthBar.transform.position = new Vector3(Camera.main.WorldToScreenPoint(transform.position).x,
        Camera.main.WorldToScreenPoint(transform.position).y+ 90, 0);
        healthBar.maxValue = health;
    }
    private void Update()
    {
        transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
        healthBar.transform.position = new Vector3(Camera.main.WorldToScreenPoint(transform.position).x,
                Camera.main.WorldToScreenPoint(transform.position).y + 90, 0);
        healthBar.value = health;
        if (health<=0)
        {
            GameObject coinText = Instantiate(coinTextPrefab, canvas.transform);
            coinText.transform.position = new Vector3(Camera.main.WorldToScreenPoint(transform.position).x,
                Camera.main.WorldToScreenPoint(transform.position).y,0);
            coinText.GetComponent<Text>().text = "+" + coin;
            tower.GetComponent<TowerController>().totalCoin += coin;
            Destroy(coinText, 1.25f);
            Destroy(healthBar.gameObject);
            Destroy(gameObject);
        }
    }

}
