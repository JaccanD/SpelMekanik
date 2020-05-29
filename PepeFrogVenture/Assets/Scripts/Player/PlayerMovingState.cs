using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

[CreateAssetMenu(menuName = "PlayerState/MovingState")]

// Author: Jacob Didenbäck
public class PlayerMovingState : PlayerBaseState
{
    [SerializeField] private float Acceleration = 4.0f;
    [SerializeField] private float MaxSpeed = 5f;

    public override void Enter()
    {
        Debug.Log("moving");
        MovePlayer();
    }
    public override void Run()
    {
        RaycastHit cast = GroundCheck();
        if (/*!GroundCheck()*/cast.collider)
        {
            stateMachine.TransitionTo<PlayerFallingState>();
            return;
        }
        EventSystem.Current.FireEvent(new PlayerWalkingEvent(cast.collider.tag));
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EventSystem.Current.FireEvent(new PlayerJumpEvent());
            stateMachine.TransitionTo<PlayerJumpingState>();
            return;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Talk();
        }

        if(Direction.magnitude != 0)
            Velocity += Direction * Acceleration * Time.deltaTime;
        else
        {
            Velocity += -Velocity.normalized * Acceleration * Time.deltaTime;
        }
        
        Velocity += Gravity * Vector3.down * Time.deltaTime;

        TurnRateAdjustment();
        if (Velocity.magnitude > MaxSpeed)
        {
            Velocity = Velocity.normalized * MaxSpeed;
        }

        MovePlayer();

        Vector3 velCheck = Velocity;
        velCheck.y = 0;
        if (velCheck.magnitude < 0.1f)
        {
            stateMachine.TransitionTo<PlayerStandingState>();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ToungeFlick();
        }
    }
    //Valter
    private void TurnRateAdjustment()
    {
        float currentDirection = Vector3.Dot(Direction.normalized, Velocity.normalized);
        float turnSpeed = Mathf.Lerp(0.3f, 0.6f, currentDirection);
        Velocity += Direction * (Acceleration + turnSpeed) * Time.deltaTime;
    }
}
