using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : Damage
{

    [SerializeField] GameObject player;
    [SerializeField] float speed;
    Rigidbody rb;
    Vector3 dir;
    private void Awake()
    {
        player.GetComponent<PlayerMovement>();
        rb=this.GetComponent<Rigidbody>();
    }
    private void Start()
    {
        dir = player.transform.position - transform.position;
        dir = dir.normalized;
        rb.velocity = speed * dir;
    }
    private void Update()
    {
        Debug.Log("pos player"+ player.transform.position);
    }

}
