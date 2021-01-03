using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerController : MonoBehaviour
{
    public TMP_Text coinText;
    public Image stamina;
    public GameObject turret;
    public GameObject bulletPrefab;
    public float cooldown = 1;
    public float damage = 10;
    public int coin = 10;
    public float range;
    public int totalCoin;
    public Upgrade[] upgrade;
    bool isShot = true;
    BulletController bc;


    List<GameObject> virus = new List<GameObject>();
    bool isLocked = false;
    GameObject lockVirus;
    private void Update()
    {
        coinText.text = "Toplam Para: " + totalCoin;
        stamina.fillAmount += cooldown / 1 * Time.deltaTime;
        if (stamina.fillAmount == 1)
        {
            isShot = true;
        }

        for (int i = 0; i < upgrade.Length; i++)
        {
            if (upgrade[i].isActive)
            {
                upgrade[i].timer += Time.deltaTime;
                upgrade[i].upFillArea.fillAmount = upgrade[i].timer / upgrade[i].activeTime;

                if (upgrade[i].timer >= upgrade[i].activeTime)
                {
                    UpgradeOff(i);
                    upgrade[i].isActive = false;
                    upgrade[i].upGameObject.SetActive(false);
                }
            }

        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f) && Input.GetMouseButtonDown(0))
        {
            if (hit.collider.tag == "CoinUP")
            {
                UpgradeOn(0);
                Destroy(hit.collider.gameObject);
            }
            if (hit.collider.tag == "CooldownUP")
            {
                UpgradeOn(1);
                Destroy(hit.collider.gameObject);
            }
            if (hit.collider.tag == "DamageUP")
            {
                UpgradeOn(2);
                Destroy(hit.collider.gameObject);
            }
        }



        for (int i = virus.Count-1; i >= 0; i--)
        {
            if (virus[i] != null)
            {
                if (inDistance(virus[i].transform, range) && !isLocked)
                {
                    lockVirus = virus[i];
                    isLocked = true;
                }
            }
            else
            {
                virus.RemoveAt(i);
            }
        }


        if (lockVirus!=null && inDistance(lockVirus.transform, range))
        {
            Shot(lockVirus);
        }
        else
        {
            isLocked = false;
        }
    }

    private void Shot(GameObject lv)
    {
        if (isShot)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = turret.transform.position;
            bc = bullet.GetComponent<BulletController>();
            bc.target = lv.transform.position;
            bc.damage = damage;
            bc.coin = coin;
            stamina.fillAmount = 0;
            isShot = false;
        }
    }

    private void UpgradeOn(int id)
    {
        switch (id)
        {
            case 0:
                if (upgrade[0].isActive)
                {
                    upgrade[0].timer = 0;
                }
                else
                {
                    upgrade[0].upGameObject.SetActive(true);
                    upgrade[0].isActive = true;
                    upgrade[0].timer = 0;
                    coin *= 2;
                }
                break;
            case 1:
                if (upgrade[1].isActive)
                {
                    upgrade[1].timer = 0;
                }
                else
                {
                    upgrade[1].upGameObject.SetActive(true);
                    upgrade[1].isActive = true;
                    upgrade[1].timer = 0;
                    cooldown *= 2;
                }
                break;
            case 2:
                if (upgrade[2].isActive)
                {
                    upgrade[2].timer = 0;
                }
                else
                {
                    upgrade[2].upGameObject.SetActive(true);
                    upgrade[2].isActive = true;
                    upgrade[2].timer = 0;
                    damage *= 2;
                }
                break;
            default:
                break;
        }
    }

    private void UpgradeOff(int id)
    {
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
    
    private bool inDistance(Transform vi,float dis)
    {
        Vector3 vec = vi.position - transform.position;
        return Vector3.Dot(transform.right, vec) <= dis && Vector3.Dot(transform.right, vec) >= 0;
    }

    public void VirusAdd(GameObject v)
    {
        virus.Add(v);
    }

}


[Serializable]
public class Upgrade
{
    public float activeTime;
    public float timer = 0;
    public bool isActive = false;
    public GameObject upGameObject;
    public Image upFillArea;
}
