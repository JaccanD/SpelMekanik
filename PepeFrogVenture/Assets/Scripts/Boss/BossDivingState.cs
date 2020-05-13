using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BossState/DivingState")]
// Author: Valter Falsterljung
public class BossDivingState : BossBaseState
{
    private Lilypads sinkTarget;
    [SerializeField] private float sinkSpeed = 1.5f;
    [SerializeField] private float rotationSpeed = 3;
    [SerializeField] private float threshold = 5f;

    public override void Run()
    {
        RotateTowardPlayer(Player.transform.position);
        DiveDown();
        if(Boss.transform.position.y < threshold)
        {
            stateMachine.TransitionTo<BossEmergingState>();
        }
    }
    private void RotateTowardPlayer(Vector3 rotateTowards)
    {
        Quaternion rotation = Quaternion.LookRotation((rotateTowards - Boss.transform.position).normalized);
        Boss.transform.rotation = Quaternion.Slerp(Boss.transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }
    private void DiveDown()
    {
        
        Boss.transform.position += Vector3.down * sinkSpeed * Time.deltaTime;
    }
}
