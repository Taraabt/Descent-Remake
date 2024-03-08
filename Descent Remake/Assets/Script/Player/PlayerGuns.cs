using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGuns : MonoBehaviour
{
    int index1 = 0;
    int index2 = 0;

    public PrimaryGun primary;
    public PrimaryGun secondary;

    Gun gun1, gun2;
    MagType mag1, mag2;

    bool
        reloadedPrimary = true,
        reloadedSecondary = true;


    // Update is called once per frame
    void Update()
    {
        int changeWeapon = (int)Input.GetAxisRaw("ChangeWeapons");

        if (changeWeapon > 0)
        {

            index1 += 1;
            if (index1 > primary.Guns.Count)
            {
                index1 = 0;
                Debug.Log(index1);
            }
        }
        else if (changeWeapon < 0)
        {
            index2 -= 1;
            if (index2 < 0)
            {
                index2 = secondary.Guns.Count - 1;
                Debug.Log(index2);
                Debug.LogWarning(changeWeapon + "/" + index2);
            }
        }

        gun1 = primary.Guns[index1].gun;
        gun2 = secondary.Guns[index2].gun;

        mag1 = primary.Guns[index1].magType;
        mag2 = secondary.Guns[index2].magType;

        if (Input.GetButtonDown("Fire1") && reloadedPrimary)
        {
            gun1.Shoot(mag1);
            StartCoroutine(ReloadTime(gun1.ReloadTime, true));
        }

        if (Input.GetButtonDown("Fire2") && reloadedSecondary)
        {
            gun2.Shoot(mag2);
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


    private void OnTriggerEnter(Collider other)
    {

    }
}
