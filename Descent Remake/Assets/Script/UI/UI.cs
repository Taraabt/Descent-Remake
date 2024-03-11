using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{

    [SerializeField] PlayerGuns player;
    TMP_Text text;


    private void Start()
    {
        text =this.GetComponentInChildren<TMP_Text>(); 
    }




    private void Update()
    {
        Debug.Log(text.text);
        int ammo1 = (int)Mathf.Round(player.mag1.ammo);
        text.text= "Ammo: " +ammo1.ToString();


    }

}
