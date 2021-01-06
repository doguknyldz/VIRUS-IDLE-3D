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
    bool isShotReady = true;
    BulletController bc;


    List<GameObject> virusList = new List<GameObject>();
    bool isLocked = false;
    GameObject lockVirus;
    private void Update()
    {
        stamina.fillAmount += cooldown / 1 * Time.deltaTime;
        if (stamina.fillAmount == 1)
        {
            isShotReady = true;
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

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100.0f))
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
        }


        for (int i = virusList.Count - 1; i >= 0; i--)
        {
            if (inDistance(virusList[i].transform, range) && !isLocked)
            {
                lockVirus = virusList[i];
                isLocked = true;
            }
        }


        if (lockVirus != null && inDistance(lockVirus.transform, range))
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
        if (isShotReady)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            bc = bullet.GetComponent<BulletController>();
            bc.target = lv.transform.position;
            bc.damage = damage;
            //CHECK konuþulacak
            bc.coin = coin;
            stamina.fillAmount = 0;
            isShotReady = false;
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

    private bool inDistance(Transform virus, float dis)
    {
        Vector3 vec = (virus.position - transform.position).normalized;
        return Vector3.Distance(transform.position, virus.position) <= dis && Vector3.Dot(transform.right, vec) >= 0;
    }

    public void VirusAdd(GameObject v)
    {
        virusList.Add(v);
    }

    public void VirusRemove(GameObject v)
    {
        if (virusList.Contains(v))
        {
            virusList.Remove(v);
        }
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
