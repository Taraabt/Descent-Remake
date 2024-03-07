using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] RoomEntry entry;
    [SerializeField] RoomExit exit;
    [SerializeField] PlayerMovement player;
    [SerializeField] float speed;
    [SerializeField] float gap;

    bool checkPlayer;
    bool hasObstacle;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        checkPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {

        hasObstacle = Physics.SphereCast(transform.position,1f,Vector3.zero,out hit);
        if (checkPlayer)
        {
            if(hasObstacle==false)
            {
                float distance = Vector3.Distance(player.transform.position, transform.position);
                if (gap<distance)
                {
                    Vector3 direction = player.transform.position - transform.position;
                    transform.Translate(direction  * Time.deltaTime * speed, Space.World);
                }
            }
            else
            {
                //transform.Translate(player.transform.position * Time.deltaTime);
            }

        }

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
        Debug.Log(checkPlayer);
    }
    void ExitRoom()
    {
        checkPlayer = false;
        Debug.Log(checkPlayer);
    }



}
