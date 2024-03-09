using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomExit : MonoBehaviour
{
    public delegate void TriggerRoomEntry();
    public event TriggerRoomEntry OnRoomExit;


    private void OnTriggerEnter(Collider collision)
    {
        OnRoomExit();
    }
}
