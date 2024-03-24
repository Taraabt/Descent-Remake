using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OstageDoor : MonoBehaviour, IHp
{
    public float HP { get; set; }
    [SerializeField] float myHp;

    private void Awake()
    {
        HP = myHp;
    }


    public void HpUp(float heal)
    {

    }

    public void TakeDmg(float dmg)
    {
        HP -= dmg;
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
