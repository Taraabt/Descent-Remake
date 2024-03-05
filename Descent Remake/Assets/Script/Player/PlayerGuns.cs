using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGuns : MonoBehaviour
{
    [SerializeField] Holster[] holsters;
    int index;
    [SerializeField] Holster[,] equipedHolsters;
    Gun gun1, gun2;
    MagType mag1, mag2;

    bool
        reloadedPrimary = true,
        reloadedSecondary = true;

    void Start()
    {
        int hLenght = holsters.Length;

        if (hLenght % 2 != 0)
        {
            hLenght--;
        }


        index = 0;
        equipedHolsters = new Holster[hLenght / 2, 2];
        int x = 0;
        for (int i = 0; i < hLenght; i += 2)
        {
            equipedHolsters[x, 0] = holsters[i];
            equipedHolsters[x, 1] = holsters[i+1];
            if (x < hLenght / 2)
                x++;
            Debug.Log(holsters[i].magType.bullet.name + " " + holsters[i+1].magType.bullet.name);
        }

        Debug.Log(equipedHolsters.Length);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("ChangeWeapons") == true)
        {
            if (index + 2 < holsters.Length)
            {
                index += 2;
                reloadedPrimary = reloadedSecondary = true;
            }
        }
        else if (Input.GetButtonDown("ChangeWeapons") == true)
        {
            if (index - 2 >= 0)
            {
                index -= 2;
                reloadedPrimary = reloadedSecondary = true;
            }
        }

        gun1 = equipedHolsters[index, 0].gun;
        gun2 = equipedHolsters[index, 1].gun;

        mag1 = equipedHolsters[index, 0].magType;
        mag2 = equipedHolsters[index, 1].magType;

        if (Input.GetButtonDown("Fire1") && reloadedPrimary)
        {
            Gun primaryGun = null;
            if (equipedHolsters[index, 0].isPrimaryFire)
            {
                gun1.Shoot(mag1);
                primaryGun = gun1;
            }
            else if (equipedHolsters[index, 1].isPrimaryFire)
            {
                gun2.Shoot(mag2);
                primaryGun = gun2;
            }

            if (primaryGun != null)
            {
                StartCoroutine(ReloadTime(primaryGun.ReloadTime, reloadedPrimary));
            }
        }

        if (Input.GetButtonDown("Fire2") && reloadedSecondary)
        {
            Debug.Log("ops");
            Gun secondaryGun = null;

            if (equipedHolsters[index, 0].isPrimaryFire == false)
            {
                gun1.Shoot(mag1);
                secondaryGun = gun1;
            }
            else if (equipedHolsters[index, 1].isPrimaryFire == false)
            {

                gun2.Shoot(mag2);
                secondaryGun = gun2;
            }

            if (secondaryGun != null)
            {
                StartCoroutine(ReloadTime(secondaryGun.ReloadTime, reloadedSecondary));
            }
        }

    }

    IEnumerator ReloadTime(float time, bool isReloaded)
    {
        isReloaded = false;
        yield return new WaitForSeconds(time);
        isReloaded = true;
    }
}
