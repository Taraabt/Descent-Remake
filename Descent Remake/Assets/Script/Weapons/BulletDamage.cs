using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : Damage
{
    [SerializeField] PlayerGuns player;

    public delegate void imDead(int number);
    public event imDead OnDead;

    private void Start()
    {
        player = FindObjectOfType<PlayerGuns>();
        damage = player.gun1.dmg;
    }

    private void OnTriggerEnter(Collider other)
    {
        OnDead.Invoke(1);
        Destroy(gameObject,2);
    }
}
