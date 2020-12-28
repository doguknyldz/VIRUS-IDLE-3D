using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerController : MonoBehaviour
{
    public Text coinText;
    public Slider stamina;
    public GameObject turret;
    public GameObject bulletPrefab;
    public GameObject UpgradeTimer;
    public GameObject UpPanel;
    public float cooldown=1;
    public float damage=10;
    public int coin = 10;
    public int totalCoin;
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
                bullet.GetComponent<BulletController>().target = collision.gameObject.transform.position;
                bullet.GetComponent<BulletController>().damage = damage;
                bullet.GetComponent<BulletController>().coin = coin;
                stamina.value = 0;
                isShot = false;
            }
        }
    }

    public void Upgrade(int id)
    {
        switch (id)
        {
            case 0:
                coin *= 2;
                GameObject UpTimer0 = Instantiate(UpgradeTimer, UpPanel.transform);
                UpTimer0.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 155, 0, 255);
                UpTimer0.transform.GetChild(3).GetComponent<Text>().text = "2x \n COIN";
                StartCoroutine(SpawnUpgrade(id));
                Destroy(UpTimer0, 5);
                break;
            case 1:
                cooldown *= 2;
                GameObject UpTimer1 = Instantiate(UpgradeTimer, UpPanel.transform);
                UpTimer1.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = new Color32(0, 200, 255, 255);
                UpTimer1.transform.GetChild(3).GetComponent<Text>().text = "2x \n SPEED";
                StartCoroutine(SpawnUpgrade(id));
                Destroy(UpTimer1, 5);
                break;
            case 2:
                damage *= 2;
                GameObject UpTimer2 = Instantiate(UpgradeTimer, UpPanel.transform);
                UpTimer2.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 0, 0, 255);
                UpTimer2.transform.GetChild(3).GetComponent<Text>().text = "2x \n DAMAGE";
                StartCoroutine(SpawnUpgrade(id));
                Destroy(UpTimer2, 5);
                break;
            default:
                break;
        }
    }

    IEnumerator SpawnUpgrade(int id)
    {
        yield return new WaitForSeconds(5);
        switch (id)
        {
            case 0:
                coin /= 2;
                break;
            case 1:
                cooldown /= 2;
                break;
            case 2:
                damage /= 2;
                break;
            default:
                break;
        }
    }
}
