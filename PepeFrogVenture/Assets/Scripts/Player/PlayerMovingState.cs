using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/MovingState")]
public class PlayerMovingState : PlayerBaseState
{
    [SerializeField] private float Acceleration = 4.0f;
    [SerializeField] private float MaxSpeed = 5f;
    public override void Enter()
    {
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
        if (Direction.magnitude == 0)
        {
            stateMachine.TransitionTo<PlayerStandingState>();
            return;
        }
        Velocity += Direction * Acceleration * Time.deltaTime;
        Velocity += Gravity * Vector3.down * Time.deltaTime;

        if(Velocity.magnitude > MaxSpeed)
        {
            Velocity = Velocity.normalized * MaxSpeed;
        }

        MovePlayer();


        if (Input.GetKeyDown(KeyCode.R))
        {
            ToungeFlick();
        }
    }

}
