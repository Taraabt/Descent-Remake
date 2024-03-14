using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Idle : BaseEnemyStates
{
    float x;
    float y;
    float z;

    float targetDistance;

    public override void OnEnter(EnemyAI enemy)
    {

        enemy.transform.rotation = Quaternion.Euler(0, 0, 0);
        enemy.rb = enemy.transform.GetComponent<Rigidbody>();
        enemy.startPos = enemy.transform.position;

        enemy.Mycollider = enemy.transform.GetComponent<SphereCollider>();

        NewTarget(enemy);
    }

    public override void OnUpdate(EnemyAI enemy)
    {

        targetDistance = Vector3.Distance(enemy.transform.position, enemy.target);

        if (targetDistance < 0.1f)
        {
            NewTarget(enemy);
        }
        else
        {
            
            if (Physics.Raycast(enemy.transform.position, enemy.direction.normalized, enemy.direction.magnitude))
            {
                NewTarget(enemy);
                enemy.timerTillUnstuck = 0;
            }
            else if (enemy.timerTillUnstuck > enemy.durationForUnstucking)
            {
                enemy.timerTillUnstuck = 0;
                NewTarget(enemy);
            }
            else
            {
                enemy.timerTillUnstuck += Time.deltaTime;
            }
        }


    }
    public override void OnFixedUpdate(EnemyAI enemy)
    {
        if (targetDistance > 0.1f)
        {

            enemy.transform.LookAt(enemy.target);
            enemy.direction = enemy.target - enemy.transform.position;
            enemy.rb.velocity = enemy.direction.normalized * enemy.speed * Time.fixedDeltaTime;

        }
    }

    public override void OnExit(EnemyAI enemy)
    {
        enemy.OnChangeState(new Chase());
    }

    public override void OnStay(EnemyAI enemy)
    {
        enemy.dir = enemy.player.position - enemy.transform.position;
        Physics.Raycast(enemy.transform.position, enemy.dir.normalized, out enemy.hit, enemy.Mycollider.radius);

        if (enemy.hit.transform == enemy.player)
        {
            OnExit(enemy);
        }
    }

    void NewTarget(EnemyAI enemy)
    {
        x = Random.Range(-enemy.idleRange, enemy.idleRange);
        y = Random.Range(-enemy.idleRange, enemy.idleRange);
        z = Random.Range(-enemy.idleRange, enemy.idleRange);
        enemy.target = new Vector3(enemy.startPos.x + x, enemy.startPos.y + y, enemy.startPos.z + z);
    }

}
