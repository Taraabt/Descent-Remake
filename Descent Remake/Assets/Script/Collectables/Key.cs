using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Key : MonoBehaviour ,ICollectable
{
    [SerializeField]PlayerMovement player;

    public void OnCollisionEnter()
    {
        player.hasKey = true;
        Destroy(this.gameObject);
    }



}
