using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/StandingState")]
public class PlayerStandingState : PlayerBaseState
{
    public override void Enter()
    {
        Velocity = Vector3.zero;
    }
    public override void Run()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Talk();
        }
        if (Direction.magnitude != 0)
        {
            stateMachine.TransitionTo<PlayerMovingState>();
            return;
        }
        if (!GroundCheck())
        {
            stateMachine.TransitionTo<PlayerFallingState>();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.TransitionTo<PlayerJumpingState>();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ToungeFlick();
        }
        Deccelerate();

        MovePlayer();
    }
    private void Deccelerate()
    {
        Velocity *= 0.2f;
        if (Velocity.magnitude < 0.1f)
            Velocity = Vector3.zero;
    }
}
