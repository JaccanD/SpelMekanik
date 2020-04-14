using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/JumpingState")]
public class PlayerJumpingState : PlayerBaseState
{
    [SerializeField] private float JumpForce;
    [SerializeField] private float Acceleration;
    [SerializeField] private float AirResistance;
    public override void Enter()
    {
        Debug.Log("Entered JumpingState");
        Velocity += JumpForce * Vector3.up;
    }
    public override void Run()
    {
        if(Velocity.y <= 0)
        {
            stateMachine.TransitionTo<PlayerFallingState>();
            return;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            //ToungeFlick();
        }
        Velocity += Gravity * Vector3.down * Time.deltaTime;
        Velocity += Acceleration * Direction * Time.deltaTime;
        ApplyAirResistance(AirResistance);

        MovePlayer();
        //Vector3 nextMove = CheckCollision(Velocity * Time.deltaTime);
        //transform.position += nextMove;
        }

    protected new Vector3 CheckCollision(Vector3 startingVelocity)
    {
        if (startingVelocity.magnitude < 0.001f)
        {
            return Vector3.zero;
        }
        Vector3 topPoint = transform.position + Coll.center + Vector3.up * (Coll.height / 2 - Coll.radius);
        Vector3 botPoint = transform.position + Coll.center + Vector3.down * (Coll.height / 2 - Coll.radius);
        RaycastHit cast;
        bool hit = Physics.CapsuleCast(topPoint, botPoint, Coll.radius, startingVelocity.normalized, out cast, startingVelocity.magnitude + SkinWidth, CollisionMask);
        if (hit)
        {
            bool SkinWidthHit = Physics.CapsuleCast(topPoint, botPoint, Coll.radius, -cast.normal, out RaycastHit SkinWidthCast, SkinWidth + startingVelocity.magnitude, CollisionMask);

            Vector3 normalForce = PhysicsFunctions.NormalForce(Velocity, cast.normal);
            Velocity += normalForce;
            if (SkinWidthHit) transform.position += (Vector3)(-SkinWidthCast.normal) * (SkinWidthCast.distance - SkinWidth);
            return CheckCollision(Velocity * Time.deltaTime);
        }
        else
        {
            return startingVelocity;
        }
    }
}
