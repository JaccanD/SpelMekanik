using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerControlState/MovementState")]
public class PlayerControlMovementState : PlayerControlBaseState
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

        Velocity += Direction * Acceleration * Time.deltaTime;
        Velocity += Vector3.down * Gravity * Time.deltaTime;


        MovePlayer();

        if (Velocity.magnitude < 0.1f && Direction.magnitude == 0)
        {
            Debug.Log("Entering Idle State");
            stateMachine.TransitionTo<PlayerControlIdleState>();
            return;
        }
    }
}
