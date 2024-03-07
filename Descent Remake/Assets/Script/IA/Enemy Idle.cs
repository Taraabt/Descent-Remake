using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyIdle : MonoBehaviour{


    [SerializeField] RoomEntry entry;
    [SerializeField] RoomExit exit;
    

    bool hasObstacle;
    [SerializeField] float enemySpeed;
    float targetDistance;

    Vector3 startPos;
    private Vector3 target;
    float x, y, z;

    private void Start()
    {
        startPos = transform.position;
        x = Random.Range(-4.5f, 4.5f);
        y = Random.Range(0.5f, 2.5f);
        z = Random.Range(-1f, 1f);
        target = new Vector3(x, y, z);
        hasObstacle = Physics.BoxCast(transform.position,Vector3.one*0.5f,Vector3.zero,Quaternion.identity,1f);
    }

    void Update(){
        hasObstacle = Physics.BoxCast(transform.position, Vector3.one * 0.5f, Vector3.zero, Quaternion.identity, 1f);
        targetDistance=Vector3.Distance(transform.position,target);
        if (hasObstacle==false&&targetDistance>0.01f)
        {
            Vector3 direction=target-transform.position;
            transform.Translate(direction*Time.deltaTime*enemySpeed,Space.World);
        }
        else if(hasObstacle ==true || targetDistance < 0.01f)
        {
            x = Random.Range(-4.5f, 4.5f);
            y = Random.Range(0.5f, 2.5f);
            z = Random.Range(-1f, 1f);
            target = new Vector3(x, y, z);
        }

    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.up*1.5f, new Vector3(10, 3, 3));
    }

    private void OnEnable()
    {
        entry.OnRoomEntry += Disable;
        exit.OnRoomExit += Enable;
    }

    private void OnDisable()
    {
        entry.OnRoomEntry -= Disable;
        exit.OnRoomExit -= Enable;
    }

    public void Disable()
    {
        this.enabled = false;
    }
    public void Enable()
    {       
        //this.enabled = true;
        //StartCoroutine(BackToStartPos());
    }

    //IEnumerator BackToStartPos()
    //{
    //    while (Vector3.Distance(transform.position, startPos) > 0.1f)
    //    {
    //        transform.Translate(startPos * Time.deltaTime * enemySpeed);
    //        yield return null;
    //    }
    //}

}
