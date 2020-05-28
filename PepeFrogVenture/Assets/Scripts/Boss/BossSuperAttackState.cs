using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

// Author: Valter Fallsterljung
[CreateAssetMenu(menuName ="BossState/SuperAttack")]
public class BossSuperAttackState : BossBaseState
{
    private int currentJumpPoint = 0;
    private bool isNextJumpReady = true;
    private Rigidbody rb;

    [Header("Behavior variables")]
    [SerializeField] private float rotationSpeed = 4f;
    [SerializeField] private float nextJumpHeightThreshold = -4f;
    [SerializeField] private float shootingHeightThreshold = 4f;

    [Header("Shooting variables")]
    private float currentCool;
    [SerializeField] private float cooldown = 0.3f;
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
        CheckIfBossShouldAttack();
        Jump();
    }
    private void CheckIfBossShouldAttack()
    {
        if (Position.y > shootingHeightThreshold)
        {
            attack();
        }
    }
    private void Jump()
    {
        if (isNextJumpReady)
        {
            Position = SuperJumpPoints[currentJumpPoint].transform.position;
            rb.AddForce(Vector3.up * 15, ForceMode.Impulse);
            isNextJumpReady = false;
            currentJumpPoint++;
            EventSystem.Current.FireEvent(new BossJumpingEvent());
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
        EventSystem.Current.FireEvent(new BossSuperAttackEvent());
        Shoot(projectileStartingForce, projectileDistanceMultiplier, projectileDamage, shootSpread);
        currentCool = cooldown;
    }
    private void CheckIfBelowWater()
    {
        if (Position.y < nextJumpHeightThreshold)
        {
            if (currentJumpPoint >= SuperJumpPoints.Length)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
                Position = Boss.GetStartPosition() + Vector3.down * 3;
                stateMachine.TransitionTo<BossDivingState>();
            }
            rb.velocity = Vector3.zero;
            isNextJumpReady = true;
        }
    }
}
