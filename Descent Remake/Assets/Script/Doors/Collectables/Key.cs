using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Key : MonoBehaviour, ICollectable
{
    PlayerMovement player;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out player))
        {
            Collect();
        }
    }

    public void Collect()
    {
        player.hasKey = true;
        Destroy(gameObject);
    }
}
