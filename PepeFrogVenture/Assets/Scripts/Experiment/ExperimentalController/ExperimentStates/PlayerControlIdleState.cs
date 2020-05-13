using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerControlState/IdleState")]
public class PlayerControlIdleState : PlayerControlBaseState
{
    public override void Run()
    {
        if (Input.GetKeyDown(Controlls.GetKeyBinding(Function.Jump)))
        {
            stateMachine.TransitionTo<PlayerControlJumpState>();
            return;
        }
        if (!GroundCheck())
        {
            stateMachine.TransitionTo<PlayerControlFallingState>();
            return;
        }
        if(Direction.magnitude != 0)
        {
            stateMachine.TransitionTo<PlayerControlMovementState>();
            return;
        }
    }
}
