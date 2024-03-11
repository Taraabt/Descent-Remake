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
            List<Holster> primary = player.GetComponent<PlayerGuns>().primary;
            List<Holster> secondary = player.GetComponent<PlayerGuns>().secondary;

            List<Holster> allGuns = new();

            allGuns.AddRange(primary);
            allGuns.AddRange(secondary);

            MagType otherMag = other.gameObject.GetComponent<MagType>();

            foreach (Holster findAmmo in allGuns)
            {
                if (findAmmo.magType.bullet == otherMag.bullet)
                {
                    findAmmo.magType.ammo.ammo += otherMag.ammo.ammo;
                }
            }

            Destroy(other.gameObject);
        }
    }


    void GunPickUpCheck(Collider other)
    {
        if (other.gameObject.layer == 11)// is a Magbox
        {
            List<Holster> primary = player.GetComponent<PlayerGuns>().primary;
            List<Holster> secondary = player.GetComponent<PlayerGuns>().secondary;

            List<Holster> allGuns = new();

            allGuns.AddRange(primary);
            allGuns.AddRange(secondary);

            Holster otherGun = other.gameObject.GetComponent<GunPickUp>().OtherMag;

            int i = 0;
            foreach (Holster findAmmo in allGuns)
            {
                if (findAmmo.magType.bullet == otherGun.magType.bullet)
                {
                    findAmmo.magType.ammo.ammo += otherGun.magType.ammo.ammo;
                }
                else
                {
                    i++;
                }
            }

            if (i >= allGuns.Count)
            {
                if (otherGun.isPrimary)
                {
                    player.GetComponent<PlayerGuns>().primary.Add(otherGun);
                }
                else
                {
                    player.GetComponent<PlayerGuns>().secondary.Add(otherGun);
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
