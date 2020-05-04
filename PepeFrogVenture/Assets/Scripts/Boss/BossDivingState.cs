using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BossState/DivingState")]
// Author: Valter Falsterljung
public class BossDivingState : BossBaseState
{
    private Lilypads sinkTarget;
    private float sinkSpeed = 1.5f;

    [SerializeField]private float threshold = 5f;

    public override void Enter()
    {

    }

    public override void Run()
    {
        DiveDown();
        if(Boss.transform.position.y < threshold)
        {
            stateMachine.TransitionTo<BossEmergingState>();
        }
    }

    private void DiveDown()
    {
        
        Boss.transform.position += Vector3.down * sinkSpeed * Time.deltaTime;
    }
}
