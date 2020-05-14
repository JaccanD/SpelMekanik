using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerControlState/MovementState")]
public class PlayerControlMovementState : PlayerControlBaseState
{   
    private float maxSpeed { get { return Player.MaxSpeed; } }
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

        if (Direction.magnitude != 0)
            Velocity += Direction * Acceleration * Time.deltaTime;
        else
        {
            Velocity += -Velocity.normalized * Acceleration * 2 * Time.deltaTime;
        }

        Velocity += Vector3.down * Gravity * Time.deltaTime;

        TurnRateAdjustment();
        if (Velocity.magnitude > maxSpeed)
        {
            Velocity = Velocity.normalized * maxSpeed;
        }

        MovePlayer();

        if (Velocity.magnitude < 0.1f && Direction.magnitude == 0)
        {
            stateMachine.TransitionTo<PlayerControlIdleState>();
            return;
        }
    }
    private void TurnRateAdjustment()
    {
        float currentDirection = Vector3.Dot(Direction.normalized, Velocity.normalized);
        float turnSpeed = Mathf.Lerp(0.5f, 0.8f, currentDirection);
        Velocity += Direction * (Acceleration + turnSpeed) * Time.deltaTime;
    }
}
