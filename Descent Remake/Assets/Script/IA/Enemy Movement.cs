using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{

    Rigidbody rb;
    [SerializeField] RoomEntry entry;
    [SerializeField] RoomExit exit;
    [SerializeField] PlayerMovement player;
    [SerializeField] float speed;
    [SerializeField] float gap;
    [SerializeField] float enemyRadius;

    bool checkPlayer;
    RaycastHit hit ;
    bool hasObstacle;

    // Start is called before the first frame update
    void Start()
    {
        checkPlayer = false;
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (checkPlayer)
        {
            transform.LookAt(player.transform.position);
            if (hasObstacle==false)
            {

                float distance = Vector3.Distance(player.transform.position, transform.position);
                if (gap<distance)
                {
                    Vector3 direction = player.transform.position - transform.position;
                    rb.velocity = direction;
                }
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Transform obsPos = collision.transform;

        GoBeyondObs(obsPos);
    }



    void GoBeyondObs(Transform pos)
    {







        //bool b1,b2,b3,b4;
        //Vector3 objCollSize=pos.GetComponent<Collider>().bounds.size;
        //Vector3 myCollSize = transform.GetComponent<Collider>().bounds.size;
        //if (transform.position.x+myCollSize.x/2<pos.position.x-objCollSize.x/2)
        //{
        //    Vector3 b1Pos=
        //    b1 = Physics.Raycast();                                          
        //    //left         
        //}
        //else if(transform.position.x - myCollSize.x / 2 > pos.position.x + objCollSize.x / 2)
        //{
        //    //right
        //}else if (transform.position.y - myCollSize.y / 2 > pos.position.y + objCollSize.y / 2)
        //{
        //    //up
        //}
        //else if (transform.position.y + myCollSize.y / 2 < pos.position.y - objCollSize.y / 2)
        //{
        //    //down
        //}
        //else if (transform.position.z - myCollSize.z / 2 > pos.position.z + objCollSize.z / 2)
        //{
        //    //forward
        //}
        //else if (transform.position.z + myCollSize.z / 2 <pos.position.z - objCollSize.z / 2)
        //{
        //    //back
        //}
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,1f);
    }

    private void OnEnable()
    {
        entry.OnRoomEntry += EntryRoom;
        exit.OnRoomExit += ExitRoom;
    }

    private void OnDisable()
    {
        entry.OnRoomEntry -= EntryRoom;
        exit.OnRoomExit -= ExitRoom;
    }


    void EntryRoom()
    {
        checkPlayer = true;
    }
    void ExitRoom()
    {
        checkPlayer = false;
    }



}
