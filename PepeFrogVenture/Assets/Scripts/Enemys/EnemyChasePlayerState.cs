using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/ChaseState")]
public class EnemyChasePlayerState : EnemyBaseState
{
    [SerializeField] private float attackDistance;
    [SerializeField] private float lostPlayerDistance;

    public override void Enter()
    {
        Debug.Log("Chasestate");
        Enemy.agent.isStopped = false;
    }

    public override void Run()
    {
        Enemy.agent.SetDestination(Enemy.player.transform.position);
        //Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, new Vector3(Enemy.player.transform.position.x, 0, Enemy.player.transform.position.z), speed * Time.deltaTime);
        if(Vector3.Distance(Enemy.transform.position, Enemy.player.transform.position) > lostPlayerDistance){
            stateMachine.TransitionTo<EnemyWalkingState>();
        }
        else if (Vector3.Distance(Enemy.transform.position, Enemy.player.transform.position) < attackDistance)
        {
            stateMachine.TransitionTo<EnemyAttackState>();
        }
    }
}
