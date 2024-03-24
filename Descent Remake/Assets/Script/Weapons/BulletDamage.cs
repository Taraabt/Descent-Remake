using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerGuns;

public class BulletDamage : Damage
{
    PlayerGuns player;

    public delegate void imDead(int number);
    public event imDead OnDead;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<IHp>(out var hp))
        {
            hp.TakeDmg(damage);
        }

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (Application.isPlaying)
            OnDead.Invoke(1);
    }
}
