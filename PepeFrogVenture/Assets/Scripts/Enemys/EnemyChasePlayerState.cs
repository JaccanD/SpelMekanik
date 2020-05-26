using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;
using EnemyAI;

[CreateAssetMenu(menuName = "EnemyState/ChaseState")]

// Author: Valter Fallsterljung

public class EnemyChasePlayerState : EnemyBaseState
{
    [SerializeField] private float attackDistance;
//    private float lostPlayerDistance;

    public override void Enter()
    {
        Enemy.agent.isStopped = false;
        EnemyCoordinator.current.AddEnemyInRange(Enemy);
        Debug.Log("chasestate");
    }

    public override void Run()
    {
        Enemy.agent.SetDestination(Enemy.player.transform.position);

        if (Vector3.Distance(Enemy.transform.position, Enemy.player.transform.position) > LostPlayerDistance)
        {
            stateMachine.TransitionTo<EnemyPlayerNearState>();
        }

        if (Vector3.Distance(Enemy.transform.position, Enemy.player.transform.position) < attackDistance)
        {
            stateMachine.TransitionTo<EnemyAttackState>();
        }
        //gamla
        //if(Vector3.Distance(Enemy.transform.position, Enemy.player.transform.position) > lostPlayerDistance){
        //    stateMachine.TransitionTo<EnemyWalkingState>();
        //}
    }
}
