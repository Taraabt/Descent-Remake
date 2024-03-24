using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LaserMover : MonoBehaviour
{
    [SerializeField] Transform parent;
    [SerializeField] float speed = 5f;
    Vector3 dir;

    private void Start()
    {
        dir = parent.localPosition - transform.localPosition;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, parent.position)> 0.5f)
        {
            transform.localPosition += speed * Time.deltaTime * dir;
        }
        else
        {
            this.enabled = false;
        }
    }
}
