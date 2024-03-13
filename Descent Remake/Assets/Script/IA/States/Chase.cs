using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : BaseEnemyStates
{
    public override void OnEnter(EnemyAI enemy)
    {
        
    }

    public override void OnExit(EnemyAI enemy)
    {
        throw new System.NotImplementedException();
    }

    public override void OnStay(EnemyAI enemy)
    {
        throw new System.NotImplementedException();
    }

    public override void OnUpdate(EnemyAI enemy)
    {
        enemy.dir = enemy.player.position - enemy.transform.position;
        Physics.Raycast(enemy.transform.position, enemy.dir.normalized, out enemy.hit, enemy.Mycollider.radius);

        if (enemy.hit.transform == enemy.player)
        {
            enemy.positions.Clear();
            enemy.positions.Add(player.position);
            enemy.hasPos = true;
            enemy.returnHome = false;
            enemy.distToPositons = gapToPlayer;
        }

        enemy.OnKillIdle?.Invoke();
    }
}
