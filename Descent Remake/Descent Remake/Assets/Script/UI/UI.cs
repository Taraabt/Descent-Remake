using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    [SerializeField] GameObject player;
    TMP_Text[] texts;
    Image[] images;
    [SerializeField] float minX;
    [SerializeField] float minY;
    private void Start()
    {
        texts = GetComponentsInChildren<TMP_Text>();
        images = GetComponentsInChildren<Image>();
        foreach (var image in images)
        {
            Debug.Log(image.name);
        }
    }




    private void Update()
    {
        PlayerGuns pGuns =player.GetComponent<PlayerGuns>();

        if (pGuns == null)
            return;

        Debug.Log(texts[2].text);
        int ammo1 = (int)Mathf.Round(pGuns.mag1.ammo.ammo);
        Debug.Log("cazzo palle");
        images[1].sprite=pGuns.gun1.gunSprite;
        images[7].fillAmount = Mathf.Lerp(1f, 0f, ammo1 / pGuns.mag1.ammo.baseAmmo);
        texts[0].text= ammo1.ToString();


        int ammo2 = (int)Mathf.Round(pGuns.mag2.ammo.ammo);
        texts[1].text = ammo2.ToString();


        PlayerHp playerHp = player.GetComponent<PlayerHp>();
        float hp = Mathf.Round(playerHp.HP);
        Debug.Log(images[3].name);
        float x = Mathf.Lerp(minX, 1f, hp/100);
        float y = Mathf.Lerp(minY, 1f, hp/100);
        images[3].rectTransform.localScale=new Vector3(x, y, 1f);
        texts[2].text = hp.ToString();




    }

}
