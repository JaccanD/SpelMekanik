using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

[CreateAssetMenu(menuName = "BossState/AttackingState")]
// Author: Valter Falsterljung
public class BossAttackingState : BossBaseState
{
    [SerializeField] private float rotationSpeed = 3;

    [Header("Shooting variables")]
    [SerializeField] private float cooldown;
    [SerializeField] private int shoots = 3;

    [Header("between 0 - 100")]
    [SerializeField] private float rapidAttackChance = 40f;
    [SerializeField] private float chargeAttackChance = 25f;

    [Header("At what health the boss can do special attacks")]
    [SerializeField] private int rapidAttackHealthThreshold = 15;
    [SerializeField] private int chargeAttackHealthThreshold = 11;

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

        Shoot(0);
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
        int randomRoll = Random.Range(0, 100);
        if (Boss.GetHealth() < rapidAttackHealthThreshold && randomRoll <= rapidAttackChance)
        {
            stateMachine.TransitionTo<BossRapidAttackingState>();
            return;
        }
        else if (Boss.GetHealth() < chargeAttackHealthThreshold && randomRoll <= rapidAttackChance + chargeAttackChance)
        {
            stateMachine.TransitionTo<BossChargeState>();
            return;
        }
        stateMachine.TransitionTo<BossDivingState>();
    }
}
