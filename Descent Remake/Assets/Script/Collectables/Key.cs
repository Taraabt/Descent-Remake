using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Key : MonoBehaviour ,ICollectable
{
    PlayerMovement player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>() ;
    }

    public void OnCollisionEnter()
    {
        player.hasKey = true;
        Destroy(this.gameObject);
    }



}
