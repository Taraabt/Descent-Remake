using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    bool quitting = false;

    LayerMask layersAffected;

    public float radiousExplosion;

    private void OnApplicationQuit()
    {
        quitting = true;
    }

    private void OnDestroy()
    {
        if (quitting) 
            return;

        Collider[] targets = Physics.OverlapSphere(transform.position, radiousExplosion, layersAffected);
        // remeber to hurt the enemies 
    }
}
