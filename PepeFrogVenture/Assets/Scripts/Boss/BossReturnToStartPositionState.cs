using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Callback;

// Author: Valter Fallsterljung
[CreateAssetMenu(menuName = "BossState/BossReturnToStartPositionState")]
public class BossReturnToStartPositionState : BossBaseState
{
    private Rigidbody rb;
    private BoxCollider collider;

    public override void Enter()
    {
        rb = Boss.GetComponent<Rigidbody>();
        collider = Boss.GetComponent<BoxCollider>();
        rb.AddForce(rb.velocity * -1.5f, ForceMode.Impulse);
    }
    public override void Run()
    {
        CollisionDetection();
        CheckBossHeight();
    }
    private void CollisionDetection()
    {
        Collider[] hitColliders = Physics.OverlapBox(Position - Boss.transform.forward * 2, collider.bounds.size / 2, Quaternion.identity, CollisionMask);
        if (hitColliders.Length > 0)
        {
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].tag == "Lilypad")
                {
                    hitColliders[i].GetComponentInParent<DestroyableLilypad>().DestroyLilypadNow();
                }
                else if (hitColliders[i].tag == "Player")
                {
                    EventSystem.Current.FireEvent(new PlayerHitEvent(hitColliders[i].gameObject, 10));
                }
            }
        }
    }
    private void CheckBossHeight()
    {
        if (Position.y < StartPosition.y - 5)
        {
            Position = StartPosition + Vector3.down * 5;
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            rb.useGravity = false;
            stateMachine.TransitionTo<BossDivingState>();
        }
    }
}
