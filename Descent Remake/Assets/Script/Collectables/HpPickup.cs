using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPickup : MonoBehaviour, ICollectable
{
    [SerializeField] float hpUp;
    Hp tempHp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out tempHp))
        {
            Collect();
        }
    }

    public void Collect()
    {
        tempHp.hp += hpUp;
        Destroy(gameObject);
    }
}
