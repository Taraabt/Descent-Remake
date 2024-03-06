using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyIA : MonoBehaviour{


    Collider[] collider;
    [SerializeField] float enemySpeed;

    private Vector3 target;
    float x, y, z;

    private void Start()
    {
        x = Random.Range(-5f, 5f);
        y = Random.Range(0, 4);
        z = Random.Range(-1.5f, 1.5f);
        collider = Physics.OverlapBox(Vector3.up * 1.5f, new Vector3(10, 3, 3), Quaternion.identity,1<<4);
    }


    void Update(){

        Debug.Log(collider.Length);
        target = new Vector3(x, y, z);

        if (collider.Length==0&&transform.position!=target)
        {
            transform.Translate(target.normalized*Time.deltaTime*enemySpeed);
        }    

    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.up*1.5f, new Vector3(10, 3, 3));
    }
}
