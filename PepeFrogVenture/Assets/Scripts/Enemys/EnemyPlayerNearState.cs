using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyAI;
using Callback;

[CreateAssetMenu(menuName = "EnemyState/PlayerNearState")]
public class EnemyPlayerNearState : EnemyBaseState
{
    public override void Enter()
    {
        EnemyCoordinator.current.AddEngagedEnemy(Enemy);
        EnemyCoordinator.current.RemoveEnemyInRangeOfPlayer(Enemy);
        Debug.Log("playernear");
    }
    public override void Run()
    {
        Enemy.agent.SetDestination(Enemy.player.transform.position);
        if (CanSeePlayer() && Vector3.Distance(Enemy.transform.position, Enemy.player.transform.position) < spotPlayerDistance)
        {
            EnemyCoordinator.current.AddEnemyInRange(Enemy);
            stateMachine.TransitionTo<EnemyChasePlayerState>();
        }
    }
}
