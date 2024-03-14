
using UnityEngine;

[CreateAssetMenu]
public class Ammo : ScriptableObject
{

    public float baseAmmo;
    [HideInInspector] public float ammo;

    private void OnEnable()
    {
        ammo = baseAmmo;
    }
}
