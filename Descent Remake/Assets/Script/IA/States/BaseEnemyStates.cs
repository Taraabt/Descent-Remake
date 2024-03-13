using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyStates
{
    public abstract void OnEnter(EnemyAI enemy);
    public abstract void OnUpdate(EnemyAI enemy);
    public abstract void OnExit(EnemyAI enemy);
    public abstract void OnStay(EnemyAI enemy);
}
