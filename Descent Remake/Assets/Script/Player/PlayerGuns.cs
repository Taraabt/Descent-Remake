using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGuns : MonoBehaviour
{
    int index1 = 0;
    int index2 = 0;

    public float contactDmg;

    public List<Holster> primary;
    public List<Holster> secondary;

    public Gun gun1, gun2;
    public MagType mag1, mag2;

    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform RocketSpawnPoint;
    bool
        reloadedPrimary = true,
        reloadedSecondary = true;


    private void Start()
    {
        PlayerHp.OnDeath += Death;
    }

    void Update()
    {
        float changeWeapon = 0;

        if (Input.GetButtonDown("ChangeWeapons"))
            changeWeapon = Input.GetAxisRaw("ChangeWeapons");


        if (changeWeapon > 0) // Pressed E
        {

            index1 += 1;
            if (index1 >= primary.Count)
            {
                index1 = 0;

            }
        }
        else if (changeWeapon < 0) // pressed Q
        {
            index2 += 1;
            if (index2 >= secondary.Count)
            {
                index2 = 0;
            }
        }

        gun1 = primary[index1].gun;
        gun2 = secondary[index2].gun;

        mag1 = primary[index1].magType;
        mag2 = secondary[index2].magType;


        if (Input.GetButton("Fire1") && reloadedPrimary)
        {
            gun1.Shoot(mag1, spawnPoint, transform.forward.normalized);
            StartCoroutine(ReloadTime(gun1.ReloadTime, true));

        }

        if (Input.GetButton("Fire2") && reloadedSecondary)
        {
            gun2.Shoot(mag2, RocketSpawnPoint, transform.forward.normalized);
            StartCoroutine(ReloadTime(gun2.ReloadTime, false));
        }

    }

    IEnumerator ReloadTime(float time, bool isPrimary)
    {
        if (isPrimary)
            reloadedPrimary = false;
        else
            reloadedSecondary = false;

        yield return new WaitForSeconds(time);

        if (isPrimary)
            reloadedPrimary = true;
        else
            reloadedSecondary = true;
    }


    void Death()
    {
        PlayerHp.OnDeath -= Death;
        this.enabled = false;
    }

    bool isopen = false;

    private IEnumerator open()
    {
        if (isopen == true)
        {
            yield return null;
            StopCoroutine(open());
        }

        int x = 100;
        while (10 < x)
        {
            x--;
            yield return null;
        }
        yield return new WaitForSeconds(x);
        isopen = true;
        StartCoroutine(close());
    }

    private IEnumerator close()
    {
        if (isopen == false)
        {
            yield return null;
            StopCoroutine(close());
        }

        int x = 10;
        while (x < 100)
        {
            x++;
            yield return null;
        }

        isopen = false;
    }



#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;

            Ray ray = new Ray(transform.position, transform.forward * gun1.MaxHitScanLenght);
            Gizmos.DrawRay(ray);
        }
    }
#endif
}
