using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Holster
{
    public bool IsEnemy = false;
    
    public Gun gun;

    public MagType magType;

    public bool isPrimary = true;

}
