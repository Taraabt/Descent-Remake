using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Damage
{
    public float speed;

    Vector3 pos;
    
    [HideInInspector] public Vector3 direction;

    private void Update()
    {
        pos = transform.position;

        pos += speed * Time.deltaTime * transform.forward; //direction.normalized;

        transform.position = pos;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.TryGetComponent<Hp>(out var hp))
        {
            hp.hp -= damage;
            if (hp.hp <= 0)
            {
                hp.Death();
            }
        }

        Destroy(gameObject);
    }
}
