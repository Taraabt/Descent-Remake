using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour, ICollectable
{
    [Header ("give the ammo you want to increse")]
    [SerializeField] Ammo ammoType;

    [Header("insert how much you want to increse")]
    [SerializeField] float addAmmo;

    private void OnTriggerEnter(Collider other)
    {
        Collect();
    }

    public void Collect()
    {
        ammoType.ammo += addAmmo;
        Destroy(gameObject);
    }
}
