using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Chase : BaseEnemyStates
{
    public override void OnEnter(EnemyAI enemy)
    {
        enemy.returnPositions.Insert(0, enemy.transform.position);
        enemy.playerPositions.Insert(0, enemy.player.position);
    }

    public override void OnExit(EnemyAI enemy)
    {
        enemy.OnChangeState(new ReturnHome());
    }

    public override void OnUpdate(EnemyAI enemy)
    {
        enemy.Chasing();

        if (Vector3.Distance(enemy.transform.position, enemy.player.position) > enemy.maxDistChase)
        {
            OnExit(enemy);
        }
    }

    public override void OnFixedUpdate(EnemyAI enemy)
    {
        if (enemy.playerPositions.Count > 0)
        {
            enemy.dir = enemy.playerPositions[0] - enemy.transform.position;

            enemy.transform.LookAt(enemy.playerPositions[0]);

            enemy.rb.velocity = enemy.speed * Time.fixedDeltaTime * enemy.dir.normalized;
        }
    }

    public override void OnStay(EnemyAI enemy)
    {
        enemy.dir = enemy.player.position - enemy.transform.position;

        Physics.Raycast(enemy.transform.position, enemy.dir.normalized, out enemy.hit, enemy.Mycollider.radius);

        if (enemy.hit.transform == enemy.player)
        {
            enemy.playerPositions.Clear();
            enemy.playerPositions.Add(enemy.player.position);
            enemy.distToPositons = enemy.attackRange;
        }
        
    }
}
