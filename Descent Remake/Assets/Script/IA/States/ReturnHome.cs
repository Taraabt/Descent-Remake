using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnHome : BaseEnemyStates
{
    public override void OnEnter(EnemyAI enemy)
    {
        enemy.StopChase();
    }
    public override void OnExit(EnemyAI enemy)
    {
        enemy.OnChangeState(new Idle());
    }

    public override void OnUpdate(EnemyAI enemy)
    {
        enemy.GoingHome();
    }


    public override void OnFixedUpdate(EnemyAI enemy)
    {
        if (enemy.returnPositions.Count > 0)
        {
            enemy.dir = enemy.returnPositions[0] - enemy.transform.position;

            enemy.transform.LookAt(enemy.returnPositions[0]);
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
            enemy.distToPositons = enemy.gapToPlayer;

            enemy.OnChangeState(new Chase());

        }
    }



}
