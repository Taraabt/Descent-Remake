using System.Collections;
using UnityEngine;

public class Shooting : BaseEnemyStates
{
    Vector3 newPos;

    public override void OnEnter(EnemyAI enemy)
    {
        enemy.playerPositions.Clear();
        enemy.rb.velocity = Vector3.zero;
        //enemy.reloadTimer = 0;
        //enemy.movingTimer = 0;
    }

    public override void OnExit(EnemyAI enemy)
    {
        enemy.movingTimer = 0;
        enemy.OnChangeState(new Chase());
    }

    public override void OnFixedUpdate(EnemyAI enemy)
    {
        if (newPos != Vector3.zero)
        {
            enemy.CirclePlayer(newPos);
        }
    }

    public override void OnUpdate(EnemyAI enemy)
    {
        if (Vector3.Distance(enemy.transform.position, enemy.player.position) > enemy.radius)
        {
            OnExit(enemy);
        }

        enemy.transform.LookAt(enemy.player);

        if (enemy.reloadTimer >= enemy.enemyGun.gun.ReloadTime)
        {
            enemy.enemyGun.gun.EnemyShoot(enemy.enemyGun.magType, enemy.transform, enemy.player);
            enemy.reloadTimer = 0;
        }
        else
        {
            enemy.reloadTimer += Time.deltaTime;
        }

        if (enemy.movingTimer >= enemy.MoveWaitTime)
        {
            newPos = Random.insideUnitSphere * enemy.radius;
            
        }
        else
        {
            enemy.movingTimer += Time.deltaTime;
        }

    }

    public override void OnStay(EnemyAI enemy)
    {

    }
}