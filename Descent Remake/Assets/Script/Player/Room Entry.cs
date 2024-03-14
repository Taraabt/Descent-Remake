using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEntry : MonoBehaviour
{
    public delegate void TriggerRoomEntry();
    public event TriggerRoomEntry OnRoomEntry;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<PlayerMovement>() == null)
            return;

        OnRoomEntry();
    }

}
