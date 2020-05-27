using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

// Author: Valter Falsterljung
[CreateAssetMenu(menuName = "BossState/BossDiveBombingState")]
public class BossChargeState : BossBaseState
{
    private Rigidbody rb;
    private BoxCollider collider;
    private bool hasCharged;

    [SerializeField] private float rotationSpeed = 3;
    [SerializeField] private float chargeForce = 40;
    [SerializeField] private float heightOffset = 0.1f;


    public override void Enter()
    {
        rb = Boss.GetComponent<Rigidbody>();
        collider = Boss.GetComponent<BoxCollider>();

    }
    public override void Run()
    {
        if (!hasCharged)
        {
            RotateTowardPlayer(Player.transform.position, rotationSpeed);
        }
        CheckIfBossShouldCharge();
        CollisionDetection();
    }
    private void CheckIfBossShouldCharge()
    {
        if (!hasCharged && Vector3.Dot(boss.transform.forward, (Player.transform.position - Boss.transform.position).normalized) > 0.95)
        {
            ChargeAtPlayer();
        }
    }
    private void ChargeAtPlayer()
    {
        EventSystem.Current.FireEvent(new BossChargeAttackEvent());
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddForce((Boss.transform.forward + (Boss.transform.up * heightOffset)) * chargeForce, ForceMode.Impulse);
        hasCharged = true;
    }
    private void CollisionDetection()
    {
        Collider[] hitColliders = Physics.OverlapBox(Position - Boss.transform.forward * 2, collider.bounds.size/2, Quaternion.identity, CollisionMask);
        if(hitColliders.Length > 0)
        {
            for(int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].tag == "Lilypad")
                {
                    hitColliders[i].GetComponentInParent<DestroyableLilypad>().DestroyLilypadNow();
                }
                else if (hitColliders[i].tag == "Player")
                {
                    EventSystem.Current.FireEvent(new PlayerHitEvent(hitColliders[i].gameObject, 10));
                }
                else
                {
                    hasCharged = false;
                    stateMachine.TransitionTo<BossReturnToStartPositionState>();
                }
            }
        }
    }
}
