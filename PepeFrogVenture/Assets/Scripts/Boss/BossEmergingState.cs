using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BossState/EmergingState")]
// Author: Valter Falsterljung
public class BossEmergingState : BossBaseState
{
    private DestroyableLilypad chosenLilypad;
    [SerializeField] private float depthOffset = 4f;
    [SerializeField] private float emergingSpeed = 1.5f;
    [SerializeField] private float fullyEmergedThreshold = 1f;
    [SerializeField] private float rotationSpeed = 4f;
    private bool hasSuperAttacked;
    public override void Enter()
    {
        LilyPadWithPlayer();
        chosenLilypad.BossTarget();
    }
    private void LilyPadWithPlayer()
    {
        chosenLilypad = Boss.lilypads[0];
        for (int i = 1; i < Boss.lilypads.Count; i++)
        {
            if(Vector3.Distance(Boss.lilypads[i].transform.position, Player.transform.position) < Vector3.Distance(chosenLilypad.transform.position, Player.transform.position)){
                chosenLilypad = Boss.lilypads[i];
            }
        }
    }
    public override void Run()
    {
        if (Boss.getHealth() <= 6 && Random.Range(0, 10) < 6 && hasSuperAttacked == false)
        {
            hasSuperAttacked = true;
            stateMachine.TransitionTo<BossSuperAttackState>();
        }

        RotateTowardPlayer(Player.transform.position);
        if (Boss.transform.position.y < fullyEmergedThreshold)
        {
            Emerge();
        }
        else
        {
            stateMachine.TransitionTo<BossAttackingState>();
        }
    }
    private void RotateTowardPlayer(Vector3 rotateTowards)
    {
        Quaternion rotation = Quaternion.LookRotation((rotateTowards - Boss.transform.position).normalized);
        Boss.transform.rotation = Quaternion.Slerp(Boss.transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }
    private void Emerge()
    {
        Boss.transform.position += Vector3.up * emergingSpeed * Time.deltaTime;
    }
}
