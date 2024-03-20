using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Gun : ScriptableObject
{
    public float ammoPerShot;
    public float ReloadTime = 1;
    public float MaxHitScanLenght = 1;
    public float dmg = 1;
    public Sprite gunSprite;


    public bool isProjectile;

    public void Shoot(MagType ammoType, Transform spawnPoint)
    {
        if (ammoType.ammo.ammo >= ammoPerShot)
        {
            ammoType.ammo.ammo -= ammoPerShot;

            if (isProjectile)
            {
                SpawnPojectile(ammoType, spawnPoint);
            }
            else
            {
                HitScanRay(spawnPoint);
            }
        }
    }

    public void EnemyShoot(MagType ammoType, Transform spawnPoint)
    {
        Instantiate(ammoType.bullet, spawnPoint.position, spawnPoint.root.rotation);
    }

    public void SpawnPojectile(MagType ammoType, Transform spawnPoint)
    {
        Instantiate(ammoType.bullet, spawnPoint.position, Quaternion.Euler(spawnPoint.root.eulerAngles));
    }

    public void HitScanRay(Transform spawnPoint)
    {
        RaycastHit hit;
        if (Physics.Raycast(spawnPoint.position, spawnPoint.forward, out hit, MaxHitScanLenght /* , layerMask or layer of enemies*/))
        {
            Debug.Log("i hit this", hit.transform);

            if (hit.transform.TryGetComponent<Hp>(out Hp hpClass))
            {
                hpClass.hp -= dmg;

                if (hpClass.hp <= 0)
                {
                    hpClass.Death();
                }
            }


        }
    }


}
