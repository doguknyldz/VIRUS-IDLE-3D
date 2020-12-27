using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerCS : MonoBehaviour
{
    public Text coinText;
    public Slider stamina;
    public GameObject turret;
    public GameObject bulletPrefab;
    public float cooldown=1;
    public float damage=10;
    public int totalCoin;
    public int coin=10;
    bool isShot=true;

    private void Update()
    {
        coinText.text = "Toplam Para: " + totalCoin;
        stamina.value += cooldown / 100;
        if (stamina.value == stamina.maxValue)
        {
            isShot = true;
        }
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
                bullet.GetComponent<BulletCS>().coin = coin;
                stamina.value = 0;
                isShot = false;
            }
        }
    }

}
