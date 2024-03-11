using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGuns : MonoBehaviour
{
    int index1 = 0;
    int index2 = 0;

    public List<Holster> primary;
    public List<Holster> secondary;

    Gun gun1, gun2;
    MagType mag1, mag2;

    [SerializeField] Transform gunTransform;

    bool
        reloadedPrimary = true,
        reloadedSecondary = true;


    void Update()
    {
        float changeWeapon = Input.GetAxisRaw("ChangeWeapons");



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


        if (Input.GetButtonDown("Fire1") && reloadedPrimary)
        {

            gun1.Shoot(mag1, gunTransform);
            StartCoroutine(ReloadTime(gun1.ReloadTime, true));
        }

        if (Input.GetButtonDown("Fire2") && reloadedSecondary)
        {

            gun2.Shoot(mag2, gunTransform);
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
}
