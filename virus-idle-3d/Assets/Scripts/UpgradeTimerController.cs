using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTimerController : MonoBehaviour
{
    public Image FillArea;
    public int id;
    private void Start()
    {
        switch (id)
        {
            case 0:
                FillArea.GetComponent<Image>().color = new Color32(255, 155, 0, 255);
                break;
            case 1:
                FillArea.GetComponent<Image>().color = new Color32(0, 200, 255, 255);
                break;
            case 2:
                FillArea.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
                break;
            default:
                break;
        }
        StartCoroutine(Timer());
    }

    private void Update()
    {
        if (gameObject.GetComponent<Slider>().value<=0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<Slider>().value -= 0.5f;
        StartCoroutine(Timer());
    }
}
