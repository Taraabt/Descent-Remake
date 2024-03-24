using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireKeyBillboarding : MonoBehaviour
{
    Camera yourCam;
    Vector3 dir;

    private void Awake()
    {
        yourCam = Camera.main;
    }

    private void Update()
    {

        dir = yourCam.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(dir);

    }
}
