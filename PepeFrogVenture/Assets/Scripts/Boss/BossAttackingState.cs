using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

[CreateAssetMenu(menuName = "BossState/AttackingState")]
// Author: Valter Falsterljung
public class BossAttackingState : BossBaseState
{
    [Header("Shooting variables")]
    [SerializeField] private float cooldown;
    [SerializeField] private int shoots = 3;
    [SerializeField] private float rotationSpeed = 3;

    [Header("between 0 - 100")]
    [SerializeField] private float rapidAttackChance = 40f;
    [SerializeField] private float chargeAttackChance = 40f;

    private float currentCool;
    private int shootsLeftBeforeSubmerge;

    public override void Enter()
    {
        shootsLeftBeforeSubmerge = shoots;
    }
    public override void Run()
    {
        RotateTowardPlayer(Player.transform.position, rotationSpeed);
        Attack();
    }

    private void Attack()
    {
        currentCool -= Time.deltaTime;

        if (currentCool > 0)
            return;

        Shoot(projectileStartingForce, projectileDistanceForceMultiplier, projectileDamage, 0);
        EventSystem.Current.FireEvent(new BossShootingEvent());
        shootsLeftBeforeSubmerge -= 1;
        currentCool = cooldown;
        if (shootsLeftBeforeSubmerge < 1)
        {
            ChooseSpecialAttack();
        }

    }
    private void ChooseSpecialAttack()
    {
        if (Boss.getHealth() < 15 && Random.Range(0, 100) <= rapidAttackChance)
        {
            stateMachine.TransitionTo<BossRapidAttackingState>();
            return;
        }
        if (Boss.getHealth() < 11 && Random.Range(0, 100) <= chargeAttackChance)
        {
            stateMachine.TransitionTo<BossChargeState>();
            return;
        }
        stateMachine.TransitionTo<BossDivingState>();
    }
}
