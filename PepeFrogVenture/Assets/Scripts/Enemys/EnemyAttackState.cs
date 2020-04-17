using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

[CreateAssetMenu(menuName = "EnemyState/AttackState")]
public class EnemyAttackState : EnemyBaseState
{
    [SerializeField] private float chaseDistance;
    [SerializeField] private float cooldown;
    private float currentCool;

    public override void Enter()
    {
        currentCool = 0.4f;
        Debug.Log("attackingstate");
        Enemy.agent.isStopped = true;
    }

    public override void Run()
    {
        
        if (!CanSeePlayer())
            stateMachine.TransitionTo<EnemyWalkingState>();
        if (Vector3.Distance(Enemy.transform.position, Enemy.player.transform.position) > chaseDistance)
            stateMachine.TransitionTo<EnemyChasePlayerState>();
        Attack();
    }

    private void Attack()
    {
        currentCool -= Time.deltaTime;

        if (currentCool > 0)
            return;

        EventSystem.Current.FireEvent(new PlayerHitEvent(Enemy.player.gameObject, Enemy.getDamage()));
        currentCool = cooldown;
    }
}
