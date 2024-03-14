using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OstageDoor : MonoBehaviour , IDamageable
{
    [SerializeField]float hp;

    public void OnCollisionEnter(Collision collision)
    {
        //hp -= collision.gameObject.damage;
        hp--;
    }

    void Update()
    {
        if (hp < 0)
        {
            Destroy(gameObject);
        }
}
    }
