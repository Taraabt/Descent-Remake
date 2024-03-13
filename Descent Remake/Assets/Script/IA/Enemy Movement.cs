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
    [SerializeField] float sphereRadius;
    [SerializeField] float enemyOffsetObj;
    [SerializeField] LayerMask sphereDetection;

    [SerializeField] Vector3 targetPos;
    [SerializeField] Vector3 direction;




    Vector3 Down;
    Vector3 Up;
    Vector3 BackWards;
    Vector3 Forward;
    Vector3 Left;
    Vector3 Right;



    Vector3 nextDir;
    float colliderAxisLenght;
    Collider collided;

    bool checkPlayer;
    bool firstTargetPos = true;


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
                    direction = direction.normalized;
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
                    direction = direction.normalized;
                    //transform.Translate(speed * Time.deltaTime * direction);
                    rb.velocity = speed * Time.deltaTime * direction;
                }
                else if (hasObstacle == true && firstTargetPos == true)
                {

                    targetPos = transform.position.y * transform.up + transform.forward * colliderAxisLenght /** (colliderAxisLenght + enemyOffsetObj)*/;
                    firstTargetPos = false;
                }
                else
                {
                    hasObstacle = false;
                    firstTargetPos = true;
                }
            }
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 16 || collision.gameObject.layer == 15 || collision.gameObject.layer == 6)
        {
            return;
        }


        collided = collision.collider;

        Transform objPos = collision.transform;


        GoBeyondObs(objPos);
    }





    void GoBeyondObs(Transform pos)
    {
        Vector3 objPos = pos.position;

        Vector3 objCollSize = pos.GetComponent<Collider>().bounds.size;
        Vector3 myCollSize = transform.GetComponent<Collider>().bounds.size;

        Vector3 PosNegative = new Vector3
            (
                (objPos.x - myCollSize.x / 2) - objCollSize.x / 2 - enemyOffsetObj,
                (objPos.y - myCollSize.y / 2) - objCollSize.y / 2 - enemyOffsetObj,
                (objPos.z - myCollSize.z / 2) - objCollSize.z / 2 - enemyOffsetObj
            );

        Vector3 PosPositive = new Vector3
            (
                (objPos.x + myCollSize.x / 2) + objCollSize.x / 2 + enemyOffsetObj,
                (objPos.y + myCollSize.y / 2) + objCollSize.y / 2 + enemyOffsetObj,
                (objPos.z + myCollSize.z / 2) + objCollSize.z / 2 + enemyOffsetObj
            );

        Down = new Vector3(transform.position.x, PosNegative.y, transform.position.z); // -
        Up = new Vector3(transform.position.x, PosPositive.y, transform.position.z); // +

        BackWards = new Vector3(transform.position.x, transform.position.y, PosNegative.z); // -
        Forward = new Vector3(transform.position.x, transform.position.y, PosPositive.z); // +

        Left = new Vector3(PosNegative.x, transform.position.y, transform.position.z); // -
        Right = new Vector3(PosPositive.x, transform.position.y, transform.position.z); // +

        Debug.LogWarning("d " + Down + " u " + Up + " b " + BackWards + " f " + Forward + " l " + Left + " r " + Right);



        if (Mathf.Abs(transform.position.x + myCollSize.x / 2) <= Mathf.Abs(pos.position.x - objCollSize.x / 2))
        {
            //left

            Debug.Log("left");


            colliderAxisLenght = collided.bounds.size.x;
            targetPos = FindNextPos(Up, Down, BackWards, Forward, Vector3.left);



        }
        else if (Mathf.Abs(transform.position.x - myCollSize.x / 2) >= Mathf.Abs(pos.position.x + objCollSize.x / 2))
        {
            //right
            colliderAxisLenght = collided.bounds.size.x;
            Debug.Log("right");
            targetPos = FindNextPos(Up, Down, BackWards, Forward, Vector3.right);

        }
        else if (Mathf.Abs(transform.position.y - myCollSize.y / 2) >= Mathf.Abs(pos.position.y + objCollSize.y / 2))
        {
            //up
            Debug.Log("up");

            colliderAxisLenght = collided.bounds.size.y;
            targetPos = FindNextPos(Left, Right, BackWards, Forward, Vector3.down);
        }
        else if (Mathf.Abs(transform.position.y + myCollSize.y / 2) <= Mathf.Abs(pos.position.y - objCollSize.y / 2))
        {
            //down
            Debug.Log("down");

            colliderAxisLenght = collided.bounds.size.y;

            targetPos = FindNextPos(Left, Right, BackWards, Forward, Vector3.up);
        }
        else if (Mathf.Abs(transform.position.z - myCollSize.z / 2) >= Mathf.Abs(pos.position.z + objCollSize.z / 2))
        {
            //forward
            Debug.Log("forward");

            colliderAxisLenght = collided.bounds.size.z;

            targetPos = FindNextPos(Up, Down, Left, Right, Vector3.forward);
        }
        else if (Mathf.Abs(transform.position.z + myCollSize.z / 2) <= Mathf.Abs(pos.position.z - objCollSize.z / 2))
        {
            //back
            Debug.Log("back");

            colliderAxisLenght = collided.bounds.size.z;

            targetPos = FindNextPos(Up, Down, Left, Right, Vector3.back);
        }


        hasObstacle = true;
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
        nextDir = theForwardPos;

        collided.transform.gameObject.layer = 8;

        if (Physics.OverlapSphere(v1, sphereRadius, sphereDetection).Length <= 0)
            nextPos = v1;

        else if (Physics.OverlapSphere(v2, sphereRadius, sphereDetection).Length <= 0)
            nextPos = v2;

        else if (Physics.OverlapSphere(v3, sphereRadius, sphereDetection).Length <= 0)
            nextPos = v3;

        else if (Physics.OverlapSphere(v4, sphereRadius, sphereDetection).Length <= 0)
            nextPos = v4;

        //nextPos += -theForwardPos * 2;
        //transform.position += -theForwardPos * 2;
        return (nextPos);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetPos, 0.3f);
        Gizmos.DrawRay(transform.position, transform.forward);

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + transform.forward * 2, 0.3f);

        if (collided == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(collided.transform.position + transform.forward * collided.bounds.size.z, 0.3f);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(Up, sphereRadius);
        Gizmos.DrawSphere(Down, sphereRadius);
        Gizmos.DrawSphere(Left, sphereRadius);
        Gizmos.DrawSphere(Right, sphereRadius);
        Gizmos.DrawSphere(Forward, sphereRadius);
        Gizmos.DrawSphere(BackWards, sphereRadius);

    }
#endif
}