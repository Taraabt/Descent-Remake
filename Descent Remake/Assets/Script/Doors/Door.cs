using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class Door : MonoBehaviour
{
    public delegate void OpenDoor();
    public delegate void CloseDoor();
    public event OpenDoor OnDoorOpen;
    public event OpenDoor OnCloseDoor;
    public float duration;
    public float lerpDuration;
    public bool needsKey;
    


    private void OnTriggerEnter(Collider other)
    {

        OnDoorOpen();
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.name, other.transform);
        OnDoorOpen();
    }

    private void OnTriggerExit(Collider other)
    {
        OnCloseDoor();
    }


}
