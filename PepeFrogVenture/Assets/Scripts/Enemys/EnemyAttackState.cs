using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/AttackState")]
public class EnemyAttackState : EnemyBaseState
{
    [SerializeField] private float chaseDistance;
    [SerializeField] private float cooldown;
    private float currentCool;

    public override void Enter()
    {
        currentCool = 0.4f;
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

        Enemy.player.GetComponent<PlayerKontroller3D>().GetGameController().TakeDamage(damage);
        currentCool = cooldown;
    }
}
