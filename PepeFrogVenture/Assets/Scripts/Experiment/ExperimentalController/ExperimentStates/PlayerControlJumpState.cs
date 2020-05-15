using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PlayerControlState/InAir/JumpingState")]
public class PlayerControlJumpState : PlayerControlInAirState
{
    [SerializeField] private float jumpForce;

    public override void Enter()
    {
        Velocity += Vector3.up * jumpForce;
    }
    public override void Run()
    {
        if(Velocity.y < 0)
        {
            stateMachine.TransitionTo<PlayerControlFallingState>();
        }

        Velocity += Direction * Acceleration * Time.deltaTime;
        Velocity += Vector3.down * Gravity * Time.deltaTime;
        ApplyAirResistance();
        MovePlayer();
    }
}
