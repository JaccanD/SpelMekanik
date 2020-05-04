using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/ChaseState")]

// Author: Valter Falsterljung

public class EnemyChasePlayerState : EnemyBaseState
{
    [SerializeField] private float attackDistance;
    [SerializeField] private float lostPlayerDistance;

    public override void Enter()
    {
        Enemy.agent.isStopped = false;
    }

    public override void Run()
    {
        Enemy.agent.SetDestination(Enemy.player.transform.position);
        if(Vector3.Distance(Enemy.transform.position, Enemy.player.transform.position) > lostPlayerDistance){
            stateMachine.TransitionTo<EnemyWalkingState>();
        }
        else if (Vector3.Distance(Enemy.transform.position, Enemy.player.transform.position) < attackDistance)
        {
            stateMachine.TransitionTo<EnemyAttackState>();
        }
    }
}
