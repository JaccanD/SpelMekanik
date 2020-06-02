using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

[CreateAssetMenu(menuName = "EnemyState/AttackState")]

// Author: Valter Falsterljung
public class EnemyAttackState : EnemyBaseState
{
    [SerializeField] private float stopAttackingDistance;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float startAttackCooldown;
    [SerializeField] private float pushAmount;
    [SerializeField] private float upPushAmount;
    [SerializeField] private float attackStunDuration;
    private float currentCool;

    public override void Enter()
    {
        currentCool = startAttackCooldown;
        Enemy.agent.isStopped = true;
    }
    public override void Run()
    {
        currentCool -= Time.deltaTime;

        if (Vector3.Distance(Enemy.transform.position, Enemy.player.transform.position) > stopAttackingDistance && currentCool <= 0)
        {
            stateMachine.TransitionTo<EnemyChasePlayerState>();
        }
        else
        {
            Attack();
        }
    }
    private void Attack()
    {
        if (currentCool > 0)
            return;

        EventSystem.Current.FireEvent(new Pushed(Enemy.player.gameObject, Enemy.transform.position, pushAmount, upPushAmount, attackStunDuration));
        EventSystem.Current.FireEvent(new PlayerHitEvent(Enemy.player.gameObject, Enemy.getDamage()));
        EventSystem.Current.FireEvent(new EnemyAttackingEvent(Enemy.gameObject));
        currentCool = attackCooldown;
    } 
    public override void Exit()
    {
        Enemy.agent.isStopped = false;
    }
}
