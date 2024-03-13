using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : BaseEnemyStates
{
    Vector3 target;
    float x;
    float y;
    float z;
    float range;
    public override void OnEnter(EnemyAI enemy)
    {
        enemy.rb = enemy.transform.GetComponent<Rigidbody>();
        enemy.startPos = enemy.transform.position;
        
        enemy.Mycollider = enemy.transform.GetComponent<SphereCollider>();
    }

    public override void OnUpdate(EnemyAI enemy)
    {
        
        float targetDistance = Vector3.Distance(enemy.transform.position, target);

        if (targetDistance > 0.01f)
        {
            Vector3 direction = target - enemy.transform.position;
            enemy.rb.velocity = direction * enemy.speed;
        }
        else if (targetDistance < 0.01f)
        {
            x = Random.Range(-range, range);
            y = Random.Range(-range, range);
            z = Random.Range(-range, range);
            target = new Vector3(enemy.startPos.x + x, enemy.startPos.y + y, enemy.startPos.z + z);
        }

        
    }

    public override void OnExit(EnemyAI enemy)
    {
        enemy.OnChangeState(new Chase());
    }

    public override void OnStay(EnemyAI enemy)
    {
        if (enemy.hit.transform == enemy.player)
        {
            OnExit(enemy);
        }
    }
}
