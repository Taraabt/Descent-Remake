using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class EnemyAI : MonoBehaviour
{
    List<Vector3> positions;
    List<Vector3> returnPositions;

    RaycastHit hit;

    Vector3 startPos;

    [SerializeField] EnemyIdle idle;

    [SerializeField] Transform player;
    [SerializeField] SphereCollider Mycollider;

    [SerializeField] Rigidbody rb;

    [SerializeField] float maxDistChase;
    [SerializeField] float speed;
    [SerializeField] float gapToPlayer;

    Vector3 dir;

    [SerializeField] float startDis = 0.1f;
    [SerializeField] float ChaseDuration = 70f;
    [SerializeField] float durationForUnstucking = 5f;

    delegate void KillIdle();
    delegate void RestartIdle();
    event KillIdle OnKillIdle;
    event RestartIdle OnRestartIdle;

    float distToPositons;
    float timer;

    float timerTillUnstuck;

    bool hasPos;
    bool returnHome;
    bool isFirstLostCheckDone;

    private void Start()
    {
        startPos = transform.position;
        positions = new();
        returnPositions = new() { startPos };
        distToPositons = startDis;
        OnKillIdle += StopIdle;
        OnRestartIdle += StartIdle;
    }

    private void Update()
    {

        if (hasPos)
        {
            Chasing();
            if (Vector3.Distance(transform.position, player.position) > maxDistChase)
            {
                StopChase();
            }
        }
        else if (returnHome) // HERE IS THE RETURN HOME THINGY
        {
            GoingHome();
        }

    }

    private void FixedUpdate()
    {
        if (positions.Count > 0 && hasPos)
        {
            dir = positions[0] - transform.position;

            transform.LookAt(positions[0]);

            rb.velocity = speed * Time.fixedDeltaTime * dir.normalized;
        }
        else if (returnPositions.Count > 0 && returnHome)
        {
            dir = returnPositions[0] - transform.position;

            transform.LookAt(returnPositions[0]);
            rb.velocity = speed * Time.fixedDeltaTime * dir.normalized;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        dir = player.position - transform.position;
        Physics.Raycast(transform.position, dir.normalized, out hit, Mycollider.radius);

        if (hit.transform == player)
        {
            positions.Clear();
            positions.Add(player.position);
            hasPos = true;
            returnHome = false;
            distToPositons = gapToPlayer;
            speed = 100;
            timer = 0;
        }

        OnKillIdle?.Invoke();
    }

    void StopIdle()
    {
        idle.enabled = false;
        OnKillIdle -= StopIdle;
    }

    void StartIdle()
    {
        idle.enabled = true;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void UnStuck(List<Vector3> givenPosis)
    {
        if (timerTillUnstuck > durationForUnstucking)
        {
            Vector3 newPos = givenPosis[0] * Random.Range(1f, 2f);
            givenPosis.Insert(0, newPos);
            timerTillUnstuck = 0;
        }
        else
        {
            timerTillUnstuck += Time.deltaTime;
        }
    }

    void StopChase()
    {
        timerTillUnstuck = 0;
        hasPos = false;
        returnHome = true;
        isFirstLostCheckDone = false;
        speed *= 3;
        positions.Clear();
    }


    void Chasing()
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
            if (isFirstLostCheckDone == false)
            {
                positions.Clear();
                isFirstLostCheckDone = true;
                positions.Add(player.position - (player.forward * 2));
            }
            else
            {
                if (Vector3.Distance(positions[positions.Count - 1], player.position) > startDis)
                {
                    positions.Add(player.position);
                }
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


    void GoingHome()
    {
        UnStuck(returnPositions);

        if (Vector3.Distance(transform.position, returnPositions[0]) < distToPositons)
        {
            returnPositions.RemoveAt(0);
        }

        if (returnPositions.Count <= 0 || Vector3.Distance(transform.position, startPos) < 0.5f)
        {
            transform.position = startPos;
            returnHome = false;
            returnPositions.Clear();
            OnRestartIdle?.Invoke();
            returnPositions.Add(startPos);
        }
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, dir);

        if (Application.isPlaying)
        {
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
