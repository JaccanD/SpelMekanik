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
    private float waterCheckWait = 1f;
    [SerializeField] private float rotationSpeed = 4f;
    [SerializeField] private float nextJumpThreshold = -4f;
    [SerializeField] private float shootingThreshold = 4f;
    [SerializeField] private float jumpForce = 20f;

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
            Debug.Log("isjumping");
            Position = SuperJumpPoints[currentJumpPoint].transform.position;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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

        SpreadShoot(projectileStartingForce, projectileDistanceMultiplier, projectileDamage, shootSpread);
        currentCool = cooldown;
    }
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
}
