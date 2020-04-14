using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/MovingState")]
public class PlayerMovingState : PlayerBaseState
{
    [SerializeField] private float Acceleration = 4.0f;
    public override void Enter()
    {
        Debug.Log("Entered MovingState");
    }
    public override void Run()
    {
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
        if (Input.GetKeyDown(KeyCode.F))
        {
            Talk();
        }
        Velocity += Direction * Acceleration * Time.deltaTime;
        Velocity += Gravity * Vector3.down * Time.deltaTime;

        MovePlayer();

        if (Player.GetVelocity().magnitude == 0)
        {
            stateMachine.TransitionTo<PlayerStandingState>();
            return;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ToungeFlick();
        }
    }

}
