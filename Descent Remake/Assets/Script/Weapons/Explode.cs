using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{

    [SerializeField] LayerMask layersAffected;

    public float radiousExplosion;

    float damage;

    private void Start()
    {
        damage = GetComponent<BulletDamage>().damage;
    }

    private void OnDestroy()
    {
        if (Application.isPlaying == false)
            return;

        damage = GetComponent<BulletDamage>().damage;

        Collider[] targets = Physics.OverlapSphere(transform.position, radiousExplosion, layersAffected);
        foreach (Collider target in targets)
        {
            if (target.TryGetComponent<IHp>(out var enemyHp))
            {
                enemyHp.TakeDmg(damage);
            }
        }
    }


#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radiousExplosion);
    }

#endif

}
