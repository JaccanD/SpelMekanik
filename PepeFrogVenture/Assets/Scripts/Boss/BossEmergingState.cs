using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BossState/EmergingState")]
// Author: Valter Fallsterljung
public class BossEmergingState : BossBaseState
{
    private DestroyableLilypad chosenLilypad;
    [SerializeField] private Material chosenLilypadMaterial;
    [SerializeField] private float depthOffset = 4f;
    [SerializeField] private float emergingSpeed = 1.5f;
    [SerializeField] private float fullyEmergedThreshold = 1f;
    [SerializeField] private float rotationSpeed = 4f;
    private bool hasSuperAttacked;
    public override void Enter()
    {
        LilyPadWithPlayer();
        CheckIfBossShouldSuperAttack();
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
        chosenLilypad.BossTarget();
        chosenLilypad.GetComponentInChildren<MeshRenderer>().material = chosenLilypadMaterial;
    }
    public override void Run()
    {
        RotateTowardPlayer(Player.transform.position, rotationSpeed);
        CheckBossDepth();
    }
    private void CheckBossDepth()
    {
        if (Boss.transform.position.y < fullyEmergedThreshold)
        {
            Emerge();
        }
        else
        {
            stateMachine.TransitionTo<BossAttackingState>();
        }
    }
    private void CheckIfBossShouldSuperAttack()
    {
        if (Boss.getHealth() <= 6 && hasSuperAttacked == false)
        {
            hasSuperAttacked = true;
            stateMachine.TransitionTo<BossSuperAttackState>();
        }
    }
    private void Emerge()
    {
        Boss.transform.position += Vector3.up * emergingSpeed * Time.deltaTime;
    }
}
