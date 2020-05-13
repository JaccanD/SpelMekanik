using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerControlBaseState : State
{
    protected PlayerControl player;
    protected PlayerControl Player => player = player ?? (PlayerControl)owner;

    protected Vector3 Velocity { get { return Player.Velocity; } set { Player.Velocity = value; } }
    protected Vector3 Direction { get { return new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized; } }

    protected GameObject PlayerGameObject { get { return Player.GameObject; } }
    protected CapsuleCollider Collider { get { return Player.Collider; } }

    protected float SkinWidth {  get { return Player.SkinWidth; } }
    protected float StaticFriktion { get { return Player.StaticFriction; } }
    protected float DynamicFriktion { get { return Player.DynamicFriction; } }
    protected float Gravity { get { return Player.Gravity; } }
    protected float Acceleration { get { return Player.Acceleration; } }
    protected float GroundCheckDistance { get { return Player.GroundCheckDistance; } }
    
    
    protected void MovePlayer()
    {
        CheckCollision();
        PlayerGameObject.transform.position += Velocity * Time.deltaTime;
        
    }
    protected bool GroundCheck()
    {
        Vector3 topPoint = PlayerGameObject.transform.position + Vector3.up * (Collider.height - Collider.radius); // + höjden av collider - radius
        Vector3 botPoint = PlayerGameObject.transform.position + Vector3.up * Collider.radius; // + radius

        return Physics.CapsuleCast(topPoint, botPoint, Collider.radius, Vector3.down, out RaycastHit collHit, GroundCheckDistance, Player.CollisionMask);
    }

    protected void CheckCollision()
    {
        Vector3 topPoint = PlayerGameObject.transform.position + Vector3.up * (Collider.height - Collider.radius); // + höjden av collider - radius
        Vector3 botPoint = PlayerGameObject.transform.position + Vector3.up * Collider.radius; // + radius

        float possibleMoveDistance;

        int counter = 0;
        while(Physics.CapsuleCast(topPoint, botPoint, Collider.radius, Velocity.normalized, out RaycastHit collHit, Mathf.Infinity, Player.CollisionMask))
        {
            possibleMoveDistance = SkinWidth / Vector3.Dot(Velocity.normalized, collHit.normal);
            possibleMoveDistance += collHit.distance;

            if (possibleMoveDistance > Velocity.magnitude * Time.deltaTime)
                break; // Rör som vanligt

            else if( possibleMoveDistance > 0)
            {
                PlayerGameObject.transform.position += Velocity.normalized * possibleMoveDistance;
            }
            if(collHit.distance <= Velocity.magnitude * Time.deltaTime + SkinWidth)
            {
                // NormalKraft
                Vector3 normalForce = PhysicsFunctions.NormalForce(Velocity, collHit.normal);
                Velocity += normalForce;

                //Friktion
                Velocity = Friktion(Velocity, normalForce);
            }

            counter++;
            if (counter > 10)
                break;

            topPoint = PlayerGameObject.transform.position + Vector3.up * (Collider.height - Collider.radius);
            botPoint = PlayerGameObject.transform.position + Vector3.up * Collider.radius;
        }
    }
    // velocity som en parameter så att det går att köra velocity.x & velocity.z
    private Vector3 Friktion(Vector3 velocity, Vector3 normalForce)
    {
        // Om statiska friktionen är för stor
        if(velocity.magnitude <= normalForce.magnitude * StaticFriktion)
        {
            // Hastighet till noll
            velocity.x = 0;
            velocity.z = 0;
            return velocity;
        }
        velocity += -velocity.normalized * (normalForce.magnitude * DynamicFriktion);
        return velocity;
        
    }

    //Collision
    //Friktion
    //Air resistance?
}
