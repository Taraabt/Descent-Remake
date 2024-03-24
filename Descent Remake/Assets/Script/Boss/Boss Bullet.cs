using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : Damage
{

    [SerializeField] PlayerMovement player;
    [SerializeField] float speed;
    Rigidbody rb;
    Vector3 dir;
    private void Awake()
    {
        player = GameObject.FindAnyObjectByType<PlayerMovement>();
        rb=this.GetComponent<Rigidbody>();
    }
    private void Start()
    {
        dir = player.transform.position - transform.position;
        dir = dir.normalized;
        rb.velocity = speed * dir;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<IHp>(out var hp))
        {
            hp.TakeDmg(damage);
        }
        Destroy(gameObject);
    }


}
