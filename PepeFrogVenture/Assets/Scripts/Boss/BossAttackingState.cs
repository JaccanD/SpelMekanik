using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

[CreateAssetMenu(menuName = "BossState/AttackingState")]
// Author: Valter Falsterljung
public class BossAttackingState : BossBaseState
{
    protected GameObject Projectile { get { return Boss.getProjectile(); } }
    private float currentCool;
    [SerializeField] private float cooldown;
    [SerializeField] private int shoots = 3;
    [SerializeField] private float rotationSpeed = 3;
    [SerializeField] private float projectileStartingForce = 1000;
    [SerializeField] private float projectileDamage = 4;
    [SerializeField] private float projectileDistanceMultiplier = 40;
    [Header("between 0 - 10")]
    [SerializeField] private float superAttackChance = 4f;
    private int shootsLeftBeforeSubmerge;

    public override void Enter()
    {
        shootsLeftBeforeSubmerge = shoots;
    }
    public override void Run()
    {
        RotateTowardPlayer(Boss.player.transform.position);
        Attack();
    }

    private void Attack()
    {
        currentCool -= Time.deltaTime;

        if (currentCool > 0)
            return;
        ShootProjectile();
        stateMachine.TransitionTo<BossDiveBombingState>();
        shootsLeftBeforeSubmerge -= 1;
        if(shootsLeftBeforeSubmerge < 1)
        {
            if(Boss.getHealth() < 15 && Random.Range(0,10) <= superAttackChance)
            {
                stateMachine.TransitionTo<BossRapidAttackingState>();
                return;
            }
            stateMachine.TransitionTo<BossDivingState>();
        }
        currentCool = cooldown;
    }
    private void RotateTowardPlayer(Vector3 rotateTowards)
    {
        Quaternion rotation = Quaternion.LookRotation((rotateTowards - Boss.transform.position).normalized);
        Boss.transform.rotation = Quaternion.Slerp(Boss.transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }
    private void ShootProjectile()
    {
        float distance = Vector3.Distance(Boss.transform.position, Player.transform.position);
        float force = projectileStartingForce + (distance * projectileDistanceMultiplier);

        GameObject newProjectile;
        
        newProjectile = Instantiate(Projectile, Boss.getShootPoint().transform.position, Boss.getShootPoint().transform.rotation);
        newProjectile.GetComponent<BossProjectile>().SetDamage(projectileDamage);

        newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.forward * force);

        EventSystem.Current.FireEvent(new BossShootingEvent());
    }
}
