using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Gun : ScriptableObject
{
    public float ammoPerShot;
    public float ReloadTime = 1;
    public float MaxHitScanLenght = 1;
    public float dmg = 1;


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

    public void SpawnPojectile(MagType ammoType, Transform spawnPoint)
    {
        MonoBehaviour.Instantiate(ammoType.bullet, spawnPoint.position, Quaternion.Euler(spawnPoint.root.eulerAngles));
    }

    public void HitScanRay(Transform spawnPoint)
    {
        Ray ray = new Ray(spawnPoint.position, Vector3.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, MaxHitScanLenght /* , layerMask or layer of enemies*/))
        {
            //enemyHp enemy = hit.transform.GetComponent<EnemyHp>();

            //enemy.hp -= dmg;
        }
    }


}
