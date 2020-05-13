using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

[CreateAssetMenu(menuName = "BossState/BossDiveBombingState")]
public class BossDiveBombingState : BossBaseState
{
    private Rigidbody rb;
    private BoxCollider collider;
    private bool hasLaunched;

    [SerializeField] private float rotationSpeed = 3;
    [SerializeField] private float chargeForce = 40;


    public override void Enter()
    {
        rb = Boss.GetComponent<Rigidbody>();
        collider = Boss.GetComponent<BoxCollider>();

    }
    public override void Run()
    {
        RotateTowardPlayer(Player.transform.position);
        if (!hasLaunched && Vector3.Dot(boss.transform.forward, (Player.transform.position - Boss.transform.position).normalized) > 0.95)
        {
            LaunchSelfAtPlayer();
        }
        CollisionDetection();
    }

    private void RotateTowardPlayer(Vector3 rotateTowards)
    {
        Quaternion rotation = Quaternion.LookRotation((rotateTowards - Boss.transform.position).normalized);
        Boss.transform.rotation = Quaternion.Slerp(Boss.transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }
    private void LaunchSelfAtPlayer()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddForce(Boss.transform.forward * chargeForce, ForceMode.Impulse);
        hasLaunched = true;
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
                    Debug.Log("lilypadcoll");
                    hitColliders[i].GetComponentInParent<DestroyableLilypad>().DestroyLilypadNow();
                }
                else if (hitColliders[i].tag == "Player")
                {
                    EventSystem.Current.FireEvent(new PlayerHitEvent(hitColliders[i].gameObject, 10));
                }
                else
                {
                    hasLaunched = false;
                    stateMachine.TransitionTo<BossReturnToStartPositionState>();
                }
            }
        }
    }
}
