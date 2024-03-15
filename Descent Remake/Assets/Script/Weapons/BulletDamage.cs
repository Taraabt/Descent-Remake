using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : Damage
{
    [SerializeField] PlayerGuns player;
    private void Start()
    {
        player = FindObjectOfType<PlayerGuns>();
        damage = player.gun1.dmg;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
