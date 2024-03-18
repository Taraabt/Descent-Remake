using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{

    [SerializeField] GameObject player;
    TMP_Text[] texts;


    private void Start()
    {
        texts = GetComponentsInChildren<TMP_Text>();
        foreach (var text in texts)
        {
            Debug.Log(text.transform.name);
        }
    }




    private void Update()
    {
        Debug.Log(texts[0].text);
        PlayerGuns pGuns =player.GetComponent<PlayerGuns>();
        int ammo1 = (int)Mathf.Round(pGuns.mag1.ammo.ammo);
        Debug.Log("cazzo palle");
        texts[0].text= ammo1.ToString();


        int ammo2 = (int)Mathf.Round(pGuns.mag2.ammo.ammo);
        texts[1].text = ammo2.ToString();

        PlayerHp playerHp = player.GetComponent<PlayerHp>();
        int hp = (int)Mathf.Round(playerHp.hp);
        texts[2].text = hp.ToString();


    }

}
