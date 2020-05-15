using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerControlState/InAir/SwingState")]
public class PlayerControlSwingState : PlayerControlInAirState
{   
    /// <summary>
    /// The force applied in the direction of the hook.
    /// </summary>
    [SerializeField] private float toungeForce;
    /// <summary>
    /// Extra force applied in the Up direction.
    /// </summary>
    [SerializeField] private float extraUpForce;
    private Vector3 hookPoint { get { return Player.Point; } }

    public override void Enter()
    {
        // Tar bort all fart
        Velocity = Vector3.zero;

        // Lägger till ny fart
        Vector3 dir = (hookPoint - PlayerGameObject.transform.position).normalized;
        Velocity += extraUpForce * Vector3.up;
        Velocity += toungeForce * dir;
    }
    
    public override void Run()
    {
        if (Velocity.y < 0)
        {
            stateMachine.TransitionTo<PlayerControlFallingState>();
        }

        Velocity += Direction * Acceleration * Time.deltaTime;
        Velocity += Vector3.down * Gravity * Time.deltaTime;
        ApplyAirResistance();
        MovePlayer();
    }
}
