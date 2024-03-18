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
        if (other.transform.TryGetComponent<Hp>(out var hp))
        {
            hp.hp -= damage;
            if (hp.hp <= 0)
            {
                hp.Death();
            }
        }

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (Application.isPlaying)
            OnDead.Invoke(1);
    }
}
