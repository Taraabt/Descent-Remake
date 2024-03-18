using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : Hp
{
    [SerializeField] Rigidbody rb;

    public delegate void Dead();
    static public event Dead OnDeath;

    public override void Death()
    {
        OnDeath.Invoke();
        rb.velocity = Vector3.zero;

        // do the death
    }
}
