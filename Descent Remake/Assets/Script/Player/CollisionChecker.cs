using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : Hp
{

    public delegate void Dead();
    static public event Dead onDeath;

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        AmmoBoxCheck(collision);
        HpUpCheck(collision);
        GunPickUpCheck(collision);
        DamageCheck(collision);
    }

    void DamageCheck(Collision other)
    {
        if (other.gameObject.layer == 6)
        {
            hp -= other.transform.GetComponent<BulletDamage>().damage;
            if(hp < 0)
            {
                Death();
            }
        }
    }

    public override void Death()
    {
        onDeath.Invoke();
        // do the death
    }

    void AmmoBoxCheck(Collision other)
    {
        if (other.gameObject.layer == 10)// is a Magbox
        {
            List<Holster> primary = gameObject.GetComponent<PlayerGuns>().primary;
            List<Holster> secondary = gameObject.GetComponent<PlayerGuns>().secondary;

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


    void GunPickUpCheck(Collision other)
    {
        if (other.gameObject.layer == 11)// is a Magbox
        {
            List<Holster> primary = gameObject.GetComponent<PlayerGuns>().primary;
            List<Holster> secondary = gameObject.GetComponent<PlayerGuns>().secondary;

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
                    gameObject.GetComponent<PlayerGuns>().primary.Add(otherGun);
                }
                else
                {
                    gameObject.GetComponent<PlayerGuns>().secondary.Add(otherGun);
                }
            }

            Destroy(other.gameObject);
        }
    }
    void HpUpCheck(Collision other)
    {
        if (other.gameObject.layer == 12) // first aid kit
        {
            // gameObject.getcomponent<HpSystem>();
        }
    }
}
