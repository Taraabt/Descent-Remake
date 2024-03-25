using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    
    [SerializeField, Tooltip("this refers to the radius of the collision sphere ray that i use for the hitscans")] 
    float hitScanRadius;


    public bool isProjectile;

    public void Shoot(MagType ammoType, Transform spawnPoint, Vector3 dir)
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
                HitScanRay(spawnPoint, ammoType,dir);
            }
        }
    }

    public void EnemyShoot(MagType ammoType, Transform spawnPoint)
    {
        SpawnPojectile(ammoType,spawnPoint);
    }

    public void SpawnPojectile(MagType ammoType, Transform spawnPoint)
    {
        Transform projectile = Instantiate(ammoType.bullet, spawnPoint.position, spawnPoint.root.rotation);
        
        if(projectile.TryGetComponent<Damage>(out Damage projectileDmg))
        {
            projectileDmg.damage = dmg;
        }
        else
        {
            foreach (Damage projectileDamage in projectile.GetComponentsInChildren<Damage>())
            {
                projectileDamage.damage = dmg;
            }
        }

        
    }

    public void HitScanRay(Transform spawnPoint, MagType ammoType, Vector3 dir)
    {
        Instantiate(ammoType.bullet, spawnPoint.position, spawnPoint.root.rotation,spawnPoint.root);
        if (Physics.SphereCast(spawnPoint.position, hitScanRadius, dir, out RaycastHit hit, MaxHitScanLenght /* , layerMask or layer of enemies*/))
        {
            if (hit.transform.TryGetComponent(out IHp hpClass))
            {
                hpClass.TakeDmg(dmg);
            }
        }
    }


}
