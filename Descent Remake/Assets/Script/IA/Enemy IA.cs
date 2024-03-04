using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{

    
    void Start()
    {
        
    }
    void Update()
    {

        //Physics.OverlapSphere(Vector3.zero,10); 
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vector3.up*0.5f, 8f);
    }
}
