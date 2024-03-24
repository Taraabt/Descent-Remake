using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float speed;
    [SerializeField] BulletDamage[] bullets;
    float totDied = 0;


    private void Start()
    {
        bullets = GetComponentsInChildren<BulletDamage>();
        foreach (var bullet in bullets)
        {
            bullet.OnDead += AllAreDead;
        }
    }

    //private void OnEnable()
    //{
    //    foreach (var bullet in bullets)
    //    {
    //        bullet.OnDead += AllAreDead;
    //    }
    //}

    void Update()
    {
        Vector3 pos = transform.position;

        pos += speed * Time.deltaTime * transform.forward;

        //pos.z += speed * Time.deltaTime;

        transform.position = pos;
    }

    void AllAreDead(int plus)
    {
        totDied += plus;

        if (totDied >= bullets.Length)
        {
            Destroy(gameObject);
        }
    }
}
