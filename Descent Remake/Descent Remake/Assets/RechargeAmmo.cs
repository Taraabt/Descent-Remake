using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeAmmo : MonoBehaviour
{
    [SerializeField] Ammo ammo;
    [SerializeField] float ammoPerSecond;
    private void OnTriggerStay(Collider other)
    {
        if (ammo.ammo < ammo.baseAmmo)
        {
            ammo.ammo += ammoPerSecond * Time.deltaTime;
        }
    }
}
