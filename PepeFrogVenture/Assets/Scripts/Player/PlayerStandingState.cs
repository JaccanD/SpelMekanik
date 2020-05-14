using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

[CreateAssetMenu(menuName = "PlayerState/StandingState")]

// Author: Jacob Didenbäck
public class PlayerStandingState : PlayerBaseState
{
    public override void Enter()
    {
        MovePlayer();
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
            EventSystem.Current.FireEvent(new PlayerJumpEvent());
            stateMachine.TransitionTo<PlayerJumpingState>();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ToungeFlick();
        }
        //gjorde så att fiender inte kunde knuffa tillbaka spelaren när de står still
        //Deccelerate();

        MovePlayer();
    }
    private void Deccelerate()
    {
        Velocity *= 0.2f;
        if (Velocity.magnitude < 0.1f)
            Velocity = Vector3.zero;
    }
}
