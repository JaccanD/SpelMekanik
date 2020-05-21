using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

[CreateAssetMenu(menuName = "EnemyState/ChaseState")]

// Author: Valter Fallsterljung

public class EnemyChasePlayerState : EnemyBaseState
{
    [SerializeField] private float attackDistance;
//    private float lostPlayerDistance;

    public override void Enter()
    {
        Enemy.agent.isStopped = false;
        EventSystem.Current.RegisterListener(typeof(PlayerLostEvent), EnemyLostPlayer);
    }

    public override void Run()
    {
        Enemy.agent.SetDestination(Enemy.player.transform.position);
        if(!seesPlayer && Vector3.Distance(Enemy.transform.position, Enemy.player.transform.position) > spotPlayerDistance){
            seesPlayer = true;
            EventSystem.Current.FireEvent(new PlayerSeenEvent(Position));
        }
        if (seesPlayer && Vector3.Distance(Enemy.transform.position, Enemy.player.transform.position) > LostPlayerDistance)
        {
            seesPlayer = false;
            EventSystem.Current.FireEvent(new PlayerLostEvent());
        }

        if (Vector3.Distance(Enemy.transform.position, Enemy.player.transform.position) < attackDistance)
        {
            stateMachine.TransitionTo<EnemyAttackState>();
        }
        //if(Vector3.Distance(Enemy.transform.position, Enemy.player.transform.position) > lostPlayerDistance){
        //    stateMachine.TransitionTo<EnemyWalkingState>();
        //}
    }
    private void EnemyLostPlayer(Callback.Event eb)
    {
        enemiesWhoSeeThePlayer--;
    }
}
