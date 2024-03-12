using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class Door : MonoBehaviour
{
    public delegate void OpenDoor();
    public event OpenDoor OnDoorOpen;
    public float duration;
    public float lerpDuration;
    public bool needsKey;


    private void OnTriggerEnter(Collider other)
    {
        OnDoorOpen();
    }


}
