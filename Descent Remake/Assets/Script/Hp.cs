using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour
{
    public float hp = 1;

    public virtual void Death()
    {
        Destroy(gameObject);
        // do the dead
    }
}
