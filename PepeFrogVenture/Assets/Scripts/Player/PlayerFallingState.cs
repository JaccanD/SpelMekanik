using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Author: Jacob Didenbäck
[CreateAssetMenu(menuName = "PlayerState/FallingState")]
public class PlayerFallingState : PlayerBaseState
{
    [SerializeField] private float Acceleration;
    [SerializeField] private float AirResistance;
    [SerializeField] private float FallingGravity;
    public override void Run()
    {
        if(GroundCheck() && Velocity.magnitude > 0.1f)
        {
            stateMachine.TransitionTo<PlayerMovingState>();
            return;
        }
        if (GroundCheck() && Velocity.magnitude == 0)
        {
            stateMachine.TransitionTo<PlayerStandingState>();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ToungeFlick();
        }

        Velocity += FallingGravity * Vector3.down * Time.deltaTime;
        Velocity += Direction * Acceleration * Time.deltaTime;
        ApplyAirResistance(AirResistance);

        MovePlayer();
    }
    protected new Vector3 CheckCollision(Vector3 startingVelocity)
    {
        if (startingVelocity.magnitude < 0.001f)
        {
            return Vector3.zero;
        }
        Vector3 topPoint = Transform.position + Coll.center + Vector3.up * (Coll.height / 2 - Coll.radius);
        Vector3 botPoint = Transform.position + Coll.center + Vector3.down * (Coll.height / 2 - Coll.radius);
        RaycastHit cast;
        bool hit = Physics.CapsuleCast(topPoint, botPoint, Coll.radius, startingVelocity.normalized, out cast, startingVelocity.magnitude + SkinWidth, CollisionMask);
        if (hit)
        {
            bool SkinWidthHit = Physics.CapsuleCast(topPoint, botPoint, Coll.radius, -cast.normal, out RaycastHit SkinWidthCast, SkinWidth + startingVelocity.magnitude, CollisionMask);
            Vector3 normalForce = PhysicsFunctions.NormalForce(Velocity, cast.normal);
            Velocity += normalForce;
            if (SkinWidthHit) Transform.position += (Vector3)(-SkinWidthCast.normal) * (SkinWidthCast.distance - SkinWidth);
            return CheckCollision(Velocity * Time.deltaTime);
        }
        else
        {
            return startingVelocity;
        }
    }

}
