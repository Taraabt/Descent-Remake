using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OstageDoor : MonoBehaviour , IDamageable
{
    [SerializeField]float hp;

    public void OnCollisionEnter(Collision collision)
    {
        hp-=collision.gameObject.GetComponent<BulletDamage>().damage;
        //particellare danno sulla porta

    }

    void Update()
    {
        if (hp < 0)
        {
            //cambia mesh porta e aggiungi particellare
            Destroy(gameObject);
        }
}
    }
