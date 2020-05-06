﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

[CreateAssetMenu(menuName = "BossState/AttackingState")]
// Author: Valter Falsterljung
public class BossAttackingState : BossBaseState
{
    protected GameObject projectile { get { return Boss.getProjectile(); } }
    private float currentCool;
    [SerializeField] private float cooldown;
    [SerializeField] private int shootsLeftBeforeSubmerge = 3;
    [SerializeField] private float rotationSpeed = 3;
    [SerializeField] private float projectileStartingForce = 1000;
    [SerializeField] private float projectileDamage = 4;
    [SerializeField] private float projectileDistanceMultiplier = 40;
    

    public override void Run()
    {
        rotateTowardPlayer(Boss.player.transform.position);
        Attack();
    }

    private void Attack()
    {
        currentCool -= Time.deltaTime;

        if (currentCool > 0)
            return;
        ShootProjectile();

        shootsLeftBeforeSubmerge -= 1;
        if(shootsLeftBeforeSubmerge < 1)
        {
            shootsLeftBeforeSubmerge = 3;
            stateMachine.TransitionTo<BossDivingState>();
        }
        currentCool = cooldown;
    }
    private void rotateTowardPlayer(Vector3 rotateTowards)
    {
        Quaternion rotation = Quaternion.LookRotation((rotateTowards - Boss.transform.position).normalized);
        Boss.transform.rotation = Quaternion.Slerp(Boss.transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }
    private void ShootProjectile()
    {
        float distance = Vector3.Distance(Boss.transform.position, player.transform.position);
        float force = projectileStartingForce + (distance * projectileDistanceMultiplier);

        GameObject newProjectile;

        newProjectile = Instantiate(projectile, Boss.getShootPoint().transform.position, Boss.getShootPoint().transform.rotation);
        newProjectile.GetComponent<BossProjectile>().SetDamage(projectileDamage);

        Debug.Log(force);
        
        newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.forward * force);

        EventSystem.Current.FireEvent(new BossShootingEvent());
    }
}
