using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gun
{
    public float ammoPerShot;
    public float ReloadTime = 1;
    public float MaxHitScanLenght = 1;
    public float dmg = 1;

    public Transform spawnPoint;
    public bool isProjectile;

    public void Shoot(MagType ammoType)
    {
        if (ammoType.ammo >= ammoPerShot)
        {
            ammoType.ammo -= ammoPerShot;

            if (isProjectile)
            {
                SpawnPojectile(ammoType);
            }
            else
            {
                HitScanRay();
            }
        }
    }

    public void SpawnPojectile(MagType ammoType)
    {
        MonoBehaviour.Instantiate(ammoType.bullet, spawnPoint.position, Quaternion.Euler(spawnPoint.root.eulerAngles));
    }

    public void HitScanRay()
    {
        Ray ray = new Ray(spawnPoint.position, Vector3.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, MaxHitScanLenght))
        {
            //check if it has hit an enemy then
            // if (hit.transform.getcomponent<enemyScript>())
            // {
            //      dmg the enemy
            //      enemyHp -= tot;
            // }
        }
    }


}
