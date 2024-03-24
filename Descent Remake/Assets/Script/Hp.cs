using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHp

{
    public float HP { get; set; }

    public void TakeDmg(float dmg);
    public void HpUp(float heal);
}
