using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class EnemyAI : Hp
{

    [Header("If it gets stuck")]
    public List<Vector3> rayCastDirs;

    [HideInInspector] public List<Vector3> playerPositions;
    [HideInInspector] public List<Vector3> returnPositions;
    [HideInInspector] public Vector3 target;

    [HideInInspector] public Vector3 direction;

    [Header("Shooting: ")]
    public Holster enemyGun;


    [Header("Circle Player: ")]
    public float MoveWaitTime;
    public float radius;

    public float contactDmg = 2;


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

    [HideInInspector] public float reloadTimer;
    [HideInInspector] public float movingTimer;



    public void OnChangeState(BaseEnemyStates newState)
    {
        state = newState;
        state.OnEnter(this);
    }


    private void Start()
    {
        startPos = transform.position;
        playerPositions = new();
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
        player = other.transform;
        state.OnStay(this);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == (1 << 9 | 1 << 16))
        {
            hp -= collision.transform.GetComponent<BulletDamage>().damage;
            if (hp <= 0)
            {
                Death();
            }
        }

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

    #region CirclePlayer
    public void CirclePlayer(Vector3 newPos)
    {
        StartCoroutine(StartCircleing(this, newPos));
    }

    IEnumerator StartCircleing(EnemyAI enemy, Vector3 newPos)
    {
        Vector3 dir = newPos - enemy.transform.position;

        while (Vector3.Distance(enemy.transform.position, newPos) > 0.1f)
        {
            enemy.transform.position += dir * (enemy.speed * Time.deltaTime);
            yield return null;
        }
        enemy.movingTimer = 0;
    }
    #endregion

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
        playerPositions.Clear();
    }


    public void Chasing()
    {

        if (Vector3.Distance(returnPositions[0], transform.position) > startDis)
        {
            returnPositions.Insert(0, transform.position);
        }

        UnStuck(playerPositions);


        dir = player.position - transform.position;
        Physics.Raycast(transform.position, dir.normalized, out hit, Mycollider.radius);

        if (hit.transform != player)
        {
            if (Vector3.Distance(playerPositions[playerPositions.Count - 1], player.position) > startDis)
            {
                playerPositions.Add(player.position);
            }
            distToPositons = startDis;
        }

        if (playerPositions.Count > 0)
        {
            if (Vector3.Distance(transform.position, playerPositions[0]) < distToPositons)
            {
                playerPositions.RemoveAt(0);
            }
            else
            {
                dir = playerPositions[0] - transform.position;
                transform.position += dir.normalized * (speed * Time.deltaTime);
            }

        }

        if (Vector3.Distance(transform.position, player.position) < distToPositons)
        {
            OnChangeState(new Shooting());
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

            if (playerPositions.Count > 0)
            {
                Gizmos.DrawSphere(playerPositions[0], 0.5f);


                for (int i = 1; i < playerPositions.Count; i++)
                {
                    Vector3 x = playerPositions[i];
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
