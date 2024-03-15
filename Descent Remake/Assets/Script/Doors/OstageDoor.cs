using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OstageDoor : Hp
{

    //public void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("hello", collision.gameObject);
    //    BulletDamage x = collision.gameObject.GetComponent<BulletDamage>();
    //    hp -= x.damage;

    //    //particellare danno sulla porta

    //}

    private void OnTriggerEnter(Collider other)
    {
        BulletDamage x = other.gameObject.GetComponent<BulletDamage>();
        hp -= x.damage;

        if (hp <= 0)
        {
            Death();
        }
    }

    public override void Death()
    {
        // do the particles 
        base.Death();
        //Destroy(gameObject);
    }
}
