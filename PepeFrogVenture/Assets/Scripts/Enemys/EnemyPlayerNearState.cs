using UnityEngine;
using EnemyAI;

[CreateAssetMenu(menuName = "EnemyState/PlayerNearState")]
public class EnemyPlayerNearState : EnemyBaseState
{
    public override void Enter()
    {
        EnemyCoordinator.current.AddEngagedEnemy(Enemy);
        EnemyCoordinator.current.RemoveEnemyInRangeOfPlayer(Enemy);
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
