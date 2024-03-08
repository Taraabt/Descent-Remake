using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform player;
    void Update()
    {
        Vector3 pos = transform.position;

        pos += transform.forward* speed*Time.deltaTime;

        //pos.z += speed * Time.deltaTime;

        transform.position = pos;
    }
}
