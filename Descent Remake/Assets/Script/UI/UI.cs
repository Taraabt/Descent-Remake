using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{

    [SerializeField] PlayerGuns player;
    TMP_Text ammo;


    private void Start()
    {
        ammo =this.GetComponentInChildren<TMP_Text>(); 
    }




    private void Update()
    {
        Debug.Log(ammo.text);
        int ammo1 = (int)Mathf.Round(player.mag1.ammo.ammo);
        ammo.text= "Ammo: " +ammo1.ToString();


    }

}
