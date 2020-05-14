using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

[CreateAssetMenu(menuName ="BossState/SuperAttack")]
public class BossSuperAttackState : BossBaseState
{
    private int currentJumpPoint = 0;
    private bool isNextJumpReady = true;
    private Rigidbody rb;
    private float waterCheckWait = 1f;
    [SerializeField] private float rotationSpeed = 4f;
    [SerializeField] private float nextJumpThreshold = -4f;
    [SerializeField] private float shootingThreshold = 4f;

    private float currentCool;
    [SerializeField] private float cooldown = 0.3f;
    [SerializeField] private int shoots = 15;
    [SerializeField] private float projectileStartingForce = 1000;
    [SerializeField] private float projectileDamage = 4;
    [SerializeField] private float projectileDistanceMultiplier = 40;
    [SerializeField] private float shootSpread = 5;

    public override void Enter()
    {
        currentJumpPoint = 0;
        rb = Boss.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
    }
    public override void Run()
    {
        RotateTowardPlayer(Player.transform.position, rotationSpeed);
        if(Position.y > shootingThreshold)
        {
            attack();
        }
        if (isNextJumpReady)
        {
            Position = SuperJumpPoints[currentJumpPoint].transform.position;
            rb.AddForce(Vector3.up * 15, ForceMode.Impulse);
            isNextJumpReady = false;
            currentJumpPoint++;
        }
        else
        {
            CheckIfBelowWater();
        }

    }
    private void attack()
    {
        currentCool -= Time.deltaTime;

        if (currentCool > 0)
            return;
        //Shoot();
        SpreadShoot(projectileStartingForce, projectileDistanceMultiplier, projectileDamage, shootSpread);
        currentCool = cooldown;
    }
    //private void RotateTowardPlayer(Vector3 rotateTowards)
    //{
    //    Quaternion rotation = Quaternion.LookRotation((rotateTowards - Boss.transform.position).normalized);
    //    Boss.transform.rotation = Quaternion.Slerp(Boss.transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    //}
    private void CheckIfBelowWater()
    {
        if (Position.y < nextJumpThreshold)
        {
            if (currentJumpPoint >= SuperJumpPoints.Length)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
                Position = Boss.GetStartPosition();
                stateMachine.TransitionTo<BossDivingState>();
            }
            rb.velocity = Vector3.zero;
            isNextJumpReady = true;
        }
    }
    //private void Shoot()
    //{
    //    float distance = Vector3.Distance(Boss.transform.position, Player.transform.position);
    //    float force = projectileStartingForce + (distance * projectileDistanceMultiplier);

    //    GameObject newProjectile;
    //    float xRotation = Random.Range(-shootSpread, shootSpread);
    //    float yRotation = Random.Range(-shootSpread, shootSpread);
    //    float zRotation = Random.Range(-shootSpread, shootSpread);
    //    newProjectile = Instantiate(Projectile, Boss.getShootPoint().transform.position, Boss.getShootPoint().transform.rotation * Quaternion.Euler(xRotation, yRotation, zRotation));
    //    newProjectile.GetComponent<BossProjectile>().SetDamage(projectileDamage);

    //    newProjectile.GetComponent<Rigidbody>().AddForce(newProjectile.transform.forward * force);

    //    EventSystem.Current.FireEvent(new BossShootingEvent());
    //}
}
