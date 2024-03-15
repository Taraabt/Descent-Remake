using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class EnemyAI : Hp
{
    public List<Vector3> positions;
    public List<Vector3> returnPositions;
    public Vector3 target;
    public Vector3 direction;

    public float contactDmg = 2;

    public List<Vector3> rayCastDirs;

    public BaseEnemyStates state;

    public RaycastHit hit;

    public Vector3 startPos;

    public Transform player;
    public SphereCollider Mycollider;

    public Rigidbody rb;

    public float maxDistChase;
    public float speed;
    public float gapToPlayer;

    public Vector3 dir;

    public float startDis = 0.1f;
    public float durationForUnstucking = 5f;

    public float maxRayDistance = 2;
    public float idleRange = 1;



    [HideInInspector] public float distToPositons;
    [HideInInspector] public float timerTillUnstuck;
    [HideInInspector] public bool isFirstLostCheckDone;



    public void OnChangeState(BaseEnemyStates newState)
    {
        state = newState;
        state.OnEnter(this);
    }


    private void Start()
    {
        startPos = transform.position;
        positions = new();
        returnPositions = new();
        distToPositons = startDis;
        state = new Idle();
        state.OnEnter(this);
    }

    private void Update()
    {
        if (hp <= 0)
        {
            Death();
        }

        state.OnUpdate(this);
    }

    private void FixedUpdate()
    {
        state.OnFixedUpdate(this);
    }

    private void OnTriggerStay(Collider other)
    {
        state.OnStay(this);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
            hp -= collision.transform.GetComponent<BulletDamage>().damage;
    }


    // THIS IS WHERE THE FUNCTIONS START:

    public override void Death()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (Application.isPlaying == false)
            return;
        // activate particle system NOW
    }

    public void StartIdle()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }



    public void UnStuck(List<Vector3> givenPosis)
    {
        float higherDot = -9;
        RaycastHit hitted;
        Vector3 newPos = Vector3.zero;
        if (timerTillUnstuck > durationForUnstucking)
        {

            foreach (Vector3 dir in rayCastDirs)
            {
                Physics.Raycast(transform.position, dir, out hitted);

                if (Vector3.Distance(hitted.point, transform.position) > maxRayDistance)
                {
                    float dot = Vector3.Dot(dir, givenPosis[0]);
                    if (dot > higherDot)
                        higherDot = dot;
                    newPos = transform.position + dir;
                }

            }
            givenPosis.Insert(0, newPos);
            timerTillUnstuck = 0;
        }
        else
        {
            timerTillUnstuck += Time.deltaTime;
        }
    }

    public void StopChase()
    {
        timerTillUnstuck = 0;
        isFirstLostCheckDone = false;
        positions.Clear();
    }


    public void Chasing()
    {

        if (Vector3.Distance(returnPositions[0], transform.position) > startDis)
        {
            returnPositions.Insert(0, transform.position);
        }

        UnStuck(positions);


        dir = player.position - transform.position;
        Physics.Raycast(transform.position, dir.normalized, out hit, Mycollider.radius);

        if (hit.transform != player)
        {
            if (Vector3.Distance(positions[positions.Count - 1], player.position) > startDis)
            {
                positions.Add(player.position);
            }
            distToPositons = startDis;
        }

        if (positions.Count > 0)
        {
            if (Vector3.Distance(transform.position, positions[0]) < distToPositons)
            {
                positions.RemoveAt(0);
            }

        }
    }


    public void GoingHome()
    {
        UnStuck(returnPositions);

        if (Vector3.Distance(transform.position, returnPositions[0]) < distToPositons)
        {
            returnPositions.RemoveAt(0);
        }

        if (returnPositions.Count <= 0 || Vector3.Distance(transform.position, startPos) < 0.5f)
        {
            transform.position = startPos;

            returnPositions.Clear();

            returnPositions.Add(startPos);
        }
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, dir);


        Gizmos.color = Color.cyan;
        if (target != null)
        {
            Gizmos.DrawSphere(target, 0.5f);
        }

        if (Application.isPlaying)
        {

            Gizmos.color = Color.white;
            Gizmos.DrawRay(transform.position, direction.normalized);

            Gizmos.DrawSphere(transform.position + direction.normalized * direction.magnitude, 0.5f);


            Gizmos.color = Color.yellow;

            Gizmos.DrawWireSphere(transform.position, maxDistChase);

            Gizmos.color = Color.green;

            if (positions.Count > 0)
            {
                Gizmos.DrawSphere(positions[0], 0.5f);


                for (int i = 1; i < positions.Count; i++)
                {
                    Vector3 x = positions[i];
                    Gizmos.color += Color.red;
                    Gizmos.DrawSphere(x, 0.5f);
                }

            }
            if (returnPositions.Count > 0)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(returnPositions[0], 0.5f);

                if (returnPositions.Count >= 2)
                {

                    for (int i = 1; i < returnPositions.Count; i++)
                    {
                        Vector3 x = returnPositions[i];
                        Gizmos.color += Color.magenta;
                        Gizmos.DrawSphere(x, 0.5f);
                    }
                }
            }


        }


    }
#endif

}
