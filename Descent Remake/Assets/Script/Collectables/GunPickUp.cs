using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickUp : MonoBehaviour, ICollectable
{
    public Holster gunPickup;
    PlayerGuns playerHolster;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out playerHolster))
        {
            Collect();
        }
    }

    public void Collect()
    {
        if (gunPickup.isPrimary)
        {
            playerHolster.primary.Add(gunPickup);
        }
        else
        {
            playerHolster.secondary.Add(gunPickup);
        }

        Destroy(gameObject);
    }
}
