﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

[CreateAssetMenu(menuName = "BossState/AttackingState")]
// Author: Valter Falsterljung
public class BossAttackingState : BossBaseState
{
    private float currentCool;
    [SerializeField] private float cooldown;
    [SerializeField] private int shoots = 3;
    [SerializeField] private float rotationSpeed = 3;
    [SerializeField] private float projectileStartingForce = 1000;
    [SerializeField] private float projectileDamage = 4;
    [SerializeField] private float projectileDistanceMultiplier = 40;
    [Header("between 0 - 100")]
    [SerializeField] private float rapidAttackChance = 40f;
    [SerializeField] private float chargeAttackChance = 40f;
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
        //
        RegularShoot(projectileStartingForce, projectileDistanceMultiplier, projectileDamage);

        shootsLeftBeforeSubmerge -= 1;
        currentCool = cooldown;
        if (shootsLeftBeforeSubmerge < 1)
        {
            ChooseSuperAttack();
        }

    }
    private void ChooseSuperAttack()
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
