using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerControlState/IdleState")]
public class PlayerControlIdleState : PlayerControlBaseState
{
    public override void Run()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            stateMachine.TransitionTo<PlayerControlMovementState>();
            return;
        }
    }
}
