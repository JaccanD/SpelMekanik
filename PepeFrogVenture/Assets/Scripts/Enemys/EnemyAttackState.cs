using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

[CreateAssetMenu(menuName = "EnemyState/AttackState")]

// Author: Valter Falsterljung
public class EnemyAttackState : EnemyBaseState
{
    [SerializeField] private float chaseDistance;
    [SerializeField] private float playerToCloseDistance;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float startAttackCooldown;
    [SerializeField] private float pushAmount;
    [SerializeField] private float upPushAmount;
    private float currentCool;

    public override void Enter()
    {
        currentCool = startAttackCooldown;
        Enemy.agent.isStopped = true;
    }

    public override void Run()
    {
        
        if (!CanSeePlayer())
            stateMachine.TransitionTo<EnemyWalkingState>();
        if (Vector3.Distance(Enemy.transform.position, Enemy.player.transform.position) > chaseDistance)
            stateMachine.TransitionTo<EnemyChasePlayerState>();
        if(Vector3.Distance(Position, Enemy.player.transform.position) < playerToCloseDistance)
        {
            Attack();
        }
        Attack();
    }

    private void Attack()
    {
        currentCool -= Time.deltaTime;

        if (currentCool > 0)
            return;

        EventSystem.Current.FireEvent(new PlayerHitEvent(Enemy.player.gameObject, Enemy.getDamage()));
        EventSystem.Current.FireEvent(new EnemyPushesPlayerBack(Enemy.player.gameObject, Enemy.transform.position, pushAmount, upPushAmount));
        currentCool = attackCooldown;
    }
}
