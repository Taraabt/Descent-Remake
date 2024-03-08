using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    GameObject player;

    private void Start()
    {
        player = gameObject;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        AmmoBoxCheck(other);
        HpUpCheck(other);
        GunPickUpCheck(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    void AmmoBoxCheck(Collider other)
    {
        if (other.gameObject.layer == 10)// is a Magbox
        {
            PrimaryGun primary = player.GetComponent<PlayerGuns>().primary;
            PrimaryGun secondary = player.GetComponent<PlayerGuns>().secondary;

            List<Holster> allGuns = new();

            allGuns.AddRange(primary.Guns);
            allGuns.AddRange(secondary.Guns);

            MagType otherMag = other.gameObject.GetComponent<MagType>();

            foreach (Holster findAmmo in allGuns)
            {
                if (findAmmo.magType.bullet == otherMag.bullet)
                {
                    findAmmo.magType.ammo += otherMag.ammo;
                }
            }

            Destroy(other.gameObject);
        }
    }


    void GunPickUpCheck(Collider other)
    {
        if (other.gameObject.layer == 11)// is a Magbox
        {
            PrimaryGun primary = player.GetComponent<PlayerGuns>().primary;
            PrimaryGun secondary = player.GetComponent<PlayerGuns>().secondary;

            List<Holster> allGuns = new();

            allGuns.AddRange(primary.Guns);
            allGuns.AddRange(secondary.Guns);

            Holster otherMag = other.gameObject.GetComponent<GunPickUp>().OtherMag;

            int i = 0;
            foreach (Holster findAmmo in allGuns)
            {
                if (findAmmo.magType.bullet == otherMag.magType.bullet)
                {
                    findAmmo.magType.ammo += otherMag.magType.ammo;
                }
                else
                {
                    i++;
                }
            }

            if (i >= allGuns.Count)
            {
                if (otherMag.isPrimary)
                {
                    player.GetComponent<PlayerGuns>().primary.Guns.Add(otherMag);
                }
                else
                {
                    player.GetComponent<PlayerGuns>().secondary.Guns.Add(otherMag);
                }
            }

            Destroy(other.gameObject);
        }
    }
    void HpUpCheck(Collider other)
    {
        if (other.gameObject.layer == 12) // first aid kit
        {
            // player.getcomponent<HpSystem>();
        }
    }
}
