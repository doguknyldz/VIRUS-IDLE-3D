using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VirusController : MonoBehaviour
{
    GameObject tower;
    public GameObject healthBarPrefab;
    GameObject healthBar;
    Image healthBarFull;
    Canvas canvas;
    public GameObject coinTextPrefab;
    public float speed;
    float maxhealth;
    public float health;
    public int coin;

    private void Start()
    {
        maxhealth = health;
        Destroy(gameObject, 20);
        Destroy(healthBar,20);

        canvas = FindObjectOfType<Canvas>();
        tower = GameObject.FindGameObjectWithTag("Player");
        healthBar = Instantiate(healthBarPrefab, canvas.transform);
        healthBarFull=healthBar.transform.GetChild(0).GetComponent<Image>();
        healthBar.transform.position = new Vector3(Camera.main.WorldToScreenPoint(transform.position).x,
        Camera.main.WorldToScreenPoint(transform.position).y + 60, 0);
        healthBarFull.fillAmount = 1;
    }
    private void Update()
    {
        transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
        healthBar.transform.position = new Vector3(Camera.main.WorldToScreenPoint(transform.position).x,
                Camera.main.WorldToScreenPoint(transform.position).y + 60, 0);
        healthBarFull.fillAmount = health/ maxhealth;
        if (health<=0)
        {
            GameObject coinText = Instantiate(coinTextPrefab, canvas.transform);
            coinText.transform.position = new Vector3(Camera.main.WorldToScreenPoint(transform.position).x,
                Camera.main.WorldToScreenPoint(transform.position).y,0);
            coinText.GetComponent<TMP_Text>().text = "+" + coin;
            tower.GetComponent<TowerController>().totalCoin += coin;
            Destroy(coinText, 1);
            Destroy(healthBar);
            Destroy(gameObject);
        }
    }

}
