using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    Rigidbody rb;
    [SerializeField] RoomEntry entry;
    [SerializeField] RoomExit exit;
    [SerializeField] PlayerMovement player;
    [SerializeField] float speed;
    [SerializeField] float gap;
    [SerializeField] float enemyRadius;
    [SerializeField] float maxRayDistance;

    [SerializeField] Vector3 targetPos;
    [SerializeField] Vector3 direction;
    Vector3 nextDir;
    Collider collided;

    bool checkPlayer;


    RaycastHit hit;
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
            if (hasObstacle == false)
            {
                transform.LookAt(player.transform.position);

                float distance = Vector3.Distance(player.transform.position, transform.position);
                if (distance > gap)
                {
                    direction = player.transform.position - transform.position;
                    rb.velocity = speed * Time.deltaTime * direction;
                }
                else
                {
                    rb.velocity = Vector3.zero;
                }
            }
            else
            {
                

                float distance = Vector3.Distance(targetPos, transform.position);

                //transform.LookAt(targetPos);

                if (distance > 0.1f)
                {
                    Debug.Log("counter");
                    direction = targetPos - transform.position;
                    //transform.Translate(speed * Time.deltaTime * direction);
                    rb.velocity = speed * Time.deltaTime * direction;
                }
                else if (hasObstacle == true)
                {
                    targetPos = transform.position + (Vector3.forward * collided.bounds.size.z);
                }
            }
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<PlayerMovement>())
        {
            return; 
        }
        collided = collision.collider;

        Transform obsPos = collision.transform;


        GoBeyondObs(obsPos);
    }



    void GoBeyondObs(Transform pos)
    {
        Vector3 objPos = pos.position;

        Vector3 objCollSize = pos.GetComponent<Collider>().bounds.size;
        Vector3 myCollSize = transform.GetComponent<Collider>().bounds.size;

        Vector3 PosNegative = new Vector3
            (
                (objPos.x - myCollSize.x / 2) - objCollSize.x / 2,
                (objPos.y - myCollSize.y / 2) - objCollSize.y / 2,
                (objPos.z - myCollSize.z / 2) - objCollSize.z / 2
            );

        Vector3 PosPositive = new Vector3
            (
                (objPos.x + myCollSize.x / 2) + objCollSize.x / 2,
                (objPos.y + myCollSize.y / 2) + objCollSize.y / 2,
                (objPos.z + myCollSize.z / 2) + objCollSize.z / 2
            );

        Vector3 Down = new Vector3(transform.position.x, PosNegative.y, transform.position.z); // -
        Vector3 Up = new Vector3(transform.position.x, PosPositive.y, transform.position.z); // +

        Vector3 BackWards = new Vector3(transform.position.x, transform.position.y, PosNegative.z); // -
        Vector3 Forward = new Vector3(transform.position.x, transform.position.y, PosPositive.z); // +

        Vector3 Left = new Vector3(PosNegative.x, transform.position.y, transform.position.z); // -
        Vector3 Right = new Vector3(PosPositive.x, transform.position.y, transform.position.z); // +

        Debug.LogWarning("d " + Down + " u " + Up + " b " + BackWards + " f " + Forward + " l " + Left + " r " + Right);



        if (transform.position.x + myCollSize.x / 2 <= pos.position.x - objCollSize.x / 2)
        {
            //left

            Debug.Log("left");

            targetPos = FindNextPos(Up, Down, BackWards, Forward, Vector3.right);

        }
        else if (transform.position.x - myCollSize.x / 2 >= pos.position.x + objCollSize.x / 2)
        {
            //right

            Debug.Log("right");
            targetPos = FindNextPos(Up, Down, BackWards, Forward, Vector3.left);

        }
        else if (transform.position.y - myCollSize.y / 2 >= pos.position.y + objCollSize.y / 2)
        {
            //up
            Debug.Log("up");
            targetPos = FindNextPos(Left, Right, BackWards, Forward, Vector3.down);
        }
        else if (transform.position.y + myCollSize.y / 2 <= pos.position.y - objCollSize.y / 2)
        {
            //down
            Debug.Log("down");
            targetPos = FindNextPos(Left, Right, BackWards, Forward, Vector3.up);
        }
        else if (transform.position.z - myCollSize.z / 2 >= pos.position.z + objCollSize.z / 2)
        {
            //forward
            Debug.Log("forward");
            targetPos = FindNextPos(Up, Down, Left, Right, Vector3.back);
        }
        else if (transform.position.z + myCollSize.z / 2 <= pos.position.z - objCollSize.z / 2)
        {
            //back
            Debug.Log("back");
            targetPos = FindNextPos(Up, Down, Left, Right, Vector3.forward);
        }


        hasObstacle = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);

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

    private Vector3 FindNextPos(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, Vector3 theForwardPos)
    {
        Vector3 nextPos = Vector3.zero;
        nextDir += -theForwardPos * 1.5f;


        if (Physics.Raycast(v1, theForwardPos, maxRayDistance) == false)
            nextPos = v1;

        else if (Physics.Raycast(v2, theForwardPos, maxRayDistance) == false)
            nextPos = v2;

        else if (Physics.Raycast(v3, theForwardPos, maxRayDistance) == false)
            nextPos = v3;

        else if (Physics.Raycast(v4, theForwardPos, maxRayDistance) == false)
            nextPos = v4;

        nextPos += -theForwardPos * 2;
        transform.position += -theForwardPos * 2;
        return (nextPos);
    }

}
