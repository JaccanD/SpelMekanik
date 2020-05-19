using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerControlBaseState : State
{
    protected PlayerControl player;
    protected PlayerControl Player => player = player ?? (PlayerControl)owner;

    protected Vector3 Velocity { get { return Player.Velocity; } set { Player.Velocity = value; } }
    protected Vector3 Direction { get { return Player.GameObject.transform.rotation * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized; } }

    protected GameObject PlayerGameObject { get { return Player.GameObject; } }
    protected CapsuleCollider Collider { get { return Player.Collider; } }

    protected float SkinWidth {  get { return Player.SkinWidth; } }
    protected float StaticFriktion { get { return Player.StaticFriction; } }
    protected float DynamicFriktion { get { return Player.DynamicFriction; } }
    protected float Gravity { get { return Player.Gravity; } }
    protected float Acceleration { get { return Player.Acceleration; } }
    protected float GroundCheckDistance { get { return Player.GroundCheckDistance; } }

    private float tolerance = 0.3f;
    protected void MovePlayer()
    {
        Vector3 nextMove = CheckCollision(Velocity * Time.deltaTime);
        PlayerGameObject.transform.position += nextMove;
        
    }
    protected bool GroundCheck()
    {
        Vector3 topPoint = PlayerGameObject.transform.position + Vector3.up * (Collider.height - Collider.radius); // + höjden av collider - radius
        Vector3 botPoint = PlayerGameObject.transform.position + Vector3.up * Collider.radius; // + radius

        return Physics.CapsuleCast(topPoint, botPoint, Collider.radius, Vector3.down, out RaycastHit collHit, GroundCheckDistance, Player.CollisionMask);
    }
    protected Vector3 CheckCollision(Vector3 startingVelocity)
    {
        if (startingVelocity.magnitude < 0.001f)
        {
            return Vector3.zero;
        }
        Vector3 topPoint = PlayerGameObject.transform.position + Vector3.up * (Collider.height - Collider.radius); // + höjden av collider - radius
        Vector3 botPoint = PlayerGameObject.transform.position + Vector3.up * Collider.radius; // + radius
        RaycastHit cast;
        float addedDistance = GetAddedDistance(startingVelocity);
        bool hit = Physics.CapsuleCast(topPoint, botPoint, Collider.radius, startingVelocity.normalized, out cast, startingVelocity.magnitude + addedDistance, Player.CollisionMask, QueryTriggerInteraction.Ignore);

        if (hit)
        {
            bool SkinWidthHit = Physics.CapsuleCast(topPoint, botPoint, Collider.radius, -cast.normal, out RaycastHit SkinWidthCast, SkinWidth + startingVelocity.magnitude, Player.CollisionMask, QueryTriggerInteraction.Ignore);
            Vector3 normalForce = PhysicsFunctions.NormalForce(Velocity, cast.normal);
            Velocity += normalForce;
            ApplyFriktion(normalForce);
            if (SkinWidthHit) PlayerGameObject.transform.position += (Vector3)(-SkinWidthCast.normal) * (SkinWidthCast.distance - SkinWidth);
            return CheckCollision(Velocity * Time.deltaTime);
        }
        else
        {
            return startingVelocity;
        }
    }
    protected void ApplyFriktion(Vector3 normalForce)
    {
        if (normalForce.magnitude * StaticFriktion > Velocity.magnitude)
            Velocity = Vector3.zero;
        else
        {
            Velocity += normalForce.magnitude * DynamicFriktion * -Velocity.normalized;
        }
    }

    protected float GetAddedDistance(Vector3 startingVelocity)
    {
        Vector3 topPoint = PlayerGameObject.transform.position + Vector3.up * (Collider.height - Collider.radius); // + höjden av collider - radius
        Vector3 botPoint = PlayerGameObject.transform.position + Vector3.up * Collider.radius; // + radius
        bool addedDistanceCastHit = Physics.CapsuleCast(topPoint, botPoint, Collider.radius, startingVelocity.normalized, out RaycastHit addedDistanceCast, float.MaxValue, Player.CollisionMask);
        bool groundCastHit = Physics.CapsuleCast(topPoint, botPoint, Collider.radius, Vector3.down, out RaycastHit groundCast, float.MaxValue, Player.CollisionMask);
        float dot = 0;
        float addedDistance = SkinWidth;

        if (addedDistanceCastHit)
        {
            dot = Vector3.Dot(startingVelocity.normalized, -addedDistanceCast.normal);
        }

        if (dot != 0)
        {
            addedDistance = SkinWidth / dot;
        }
        return addedDistance;
    }
    //Collision
    //Friktion
    //Air resistance?
}
