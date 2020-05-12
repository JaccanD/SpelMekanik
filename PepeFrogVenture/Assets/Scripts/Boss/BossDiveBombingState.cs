using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BossState/BossDiveBombingState")]
public class BossDiveBombingState : BossBaseState
{
    private Rigidbody rb;
    private BoxCollider collider;
    [SerializeField] private float rotationSpeed = 3;
    [SerializeField] private bool hasLaunched;

    public override void Enter()
    {
        rb = Boss.GetComponent<Rigidbody>();
        collider = Boss.GetComponent<BoxCollider>();

    }
    public override void Run()
    {
        RotateTowardPlayer(Player.transform.position);
        Debug.Log(Vector3.Dot(boss.transform.forward, (Player.transform.position - Boss.transform.position).normalized));
        if (Vector3.Dot(boss.transform.forward, (Player.transform.position - Boss.transform.position).normalized) > 0.95 && !hasLaunched)
        {
            LaunchSelfAtPlayer();
        }
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
        rb.AddForce(Boss.transform.forward * 30, ForceMode.Impulse);
        Debug.Log("launching");
        hasLaunched = true;
    }
}
