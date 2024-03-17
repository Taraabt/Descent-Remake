using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;

    Vector3 pos;
    
    [HideInInspector] public Vector3 direction;

    private void Update()
    {
        pos = transform.position;

        pos += speed * Time.deltaTime * direction.normalized;

        transform.position = pos;
    }
}
