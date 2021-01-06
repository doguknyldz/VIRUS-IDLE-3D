using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VirusController : MonoBehaviour
{
    public GameObject tower;
    public GameObject healthBarPrefab;
    GameObject healthBar;
    Image healthBarFull;
    public Canvas canvas;
    public GameObject coinTextPrefab;
    public float speed;
    float maxhealth;
    public float health;
    public int coin;
    TowerController tc;

    private void Start()
    {
        maxhealth = health;
        healthBar = Instantiate(healthBarPrefab, canvas.transform);
        healthBarFull = healthBar.GetComponent<HealthBarUIContainer>().healthBarImage;
        healthBar.transform.position = new Vector3(Camera.main.WorldToScreenPoint(transform.position).x,
        Camera.main.WorldToScreenPoint(transform.position).y + 60, 0);
        healthBarFull.fillAmount = 1;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
        healthBar.transform.position = new Vector3(Camera.main.WorldToScreenPoint(transform.position).x,
                Camera.main.WorldToScreenPoint(transform.position).y + 60, 0);
        healthBarFull.fillAmount = health / maxhealth;

        if (health <= 0)
        {
            GameObject coinText = Instantiate(coinTextPrefab, canvas.transform);
            coinText.transform.position = new Vector3(Camera.main.WorldToScreenPoint(transform.position).x,
                Camera.main.WorldToScreenPoint(transform.position).y, 0);
            coinText.GetComponent<TMP_Text>().text = "+" + coin;
            tc = tower.GetComponent<TowerController>();
            tc.totalCoin += coin;
            tc.coinText.text = "Toplam Para: " + tc.totalCoin;
            Destroy(coinText, 1);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Limit")
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Destroy(healthBar);
        if (tower != null)
            tower.GetComponent<TowerController>().VirusRemove(gameObject);
    }
}
