using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

[CreateAssetMenu(menuName = "BossState/RapidAttackingState")]
public class BossRapidAttackingState : BossBaseState
{
    private float currentCool;
    [SerializeField] private float cooldown = 0.3f;
    [SerializeField] private int shoots = 15;
    [SerializeField] private float rotationSpeed = 3;
    [SerializeField] private float projectileStartingForce = 1000;
    [SerializeField] private float projectileDamage = 4;
    [SerializeField] private float projectileDistanceMultiplier = 40;
    [SerializeField] private float shootSpread = 5;
    private int shootsLeftBeforeSubmerge;

    public override void Enter()
    {
        shootsLeftBeforeSubmerge = shoots;
    }
    public override void Run()
    {
        RotateTowardPlayer(Player.transform.position, rotationSpeed);
        attack();
    }
    private void attack()
    {
        currentCool -= Time.deltaTime;

        if (currentCool > 0)
            return;
        //Shoot();
        SpreadShoot(projectileStartingForce, projectileDistanceMultiplier, projectileDamage, shootSpread);
        shootsLeftBeforeSubmerge -= 1;
        if (shootsLeftBeforeSubmerge < 1)
        {
            shootsLeftBeforeSubmerge = 5;
            stateMachine.TransitionTo<BossDivingState>();
        }
        currentCool = cooldown;
    }
    //private void RotateTowardPlayer(Vector3 rotateTowards)
    //{
    //    Quaternion rotation = Quaternion.LookRotation((rotateTowards - Boss.transform.position).normalized);
    //    Boss.transform.rotation = Quaternion.Slerp(Boss.transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    //}
    //private void Shoot()
    //{
    //    float distance = Vector3.Distance(Boss.transform.position, Player.transform.position);
    //    float force = projectileStartingForce + (distance * projectileDistanceMultiplier);

    //    GameObject newProjectile;
    //    float xRotation = Random.Range(-5, 5);
    //    float yRotation = Random.Range(-5, 5);
    //    float zRotation = Random.Range(-5, 5);
    //    newProjectile = Instantiate(Projectile, Boss.getShootPoint().transform.position, Boss.getShootPoint().transform.rotation * Quaternion.Euler(xRotation, yRotation, zRotation));
    //    newProjectile.GetComponent<BossProjectile>().SetDamage(projectileDamage);

    //    newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.forward * force);

    //    EventSystem.Current.FireEvent(new BossShootingEvent());
    //}
}
