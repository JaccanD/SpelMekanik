using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PlayerControlState/InAir/FallingState")]
public class PlayerControlFallingState : PlayerControlInAirState
{
    [SerializeField] private float fallingGravity;

    public override void Run()
    {
        Velocity += Direction * Acceleration * Time.deltaTime;
        Velocity += Vector3.down * fallingGravity * Time.deltaTime;
        ApplyAirResistance();
        MovePlayer();

        if (GroundCheck())
        {
            if(Direction.magnitude == 0)
            {
                stateMachine.TransitionTo<PlayerControlIdleState>();
                return;
            }
            stateMachine.TransitionTo<PlayerControlMovementState>();
            return;
        }   
    }
}
