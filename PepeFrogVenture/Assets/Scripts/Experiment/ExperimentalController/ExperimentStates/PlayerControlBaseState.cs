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
            if(counter == 0)
                Debug.DrawLine(botPoint, collHit.point, Color.red);
            else
                Debug.DrawLine(botPoint, collHit.point, Color.green);
            possibleMoveDistance = SkinWidth / Vector3.Dot(Velocity.normalized, collHit.normal);
            possibleMoveDistance += collHit.distance;

            if (possibleMoveDistance > Velocity.magnitude * Time.deltaTime)
            {
                break; // Rör som vanligt
            }

            else if (possibleMoveDistance > 0)
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
            CollisionOverlap();
            
            counter++;
            if (counter > 10)
                break;

            topPoint = PlayerGameObject.transform.position + Vector3.up * (Collider.height - Collider.radius);
            botPoint = PlayerGameObject.transform.position + Vector3.up * Collider.radius;
        }

        if (counter > 0)
            CollisionOverlap();
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

    private void CollisionOverlap()
    {
        Vector3 topPoint = PlayerGameObject.transform.position + Vector3.up * (Collider.height - Collider.radius); // + höjden av collider - radius
        Vector3 botPoint = PlayerGameObject.transform.position + Vector3.up * Collider.radius; // + radius

        // Kollar om någon av punkterna är närmare än skinwidth från någon collider
        bool checkTopPoint = Physics.CheckSphere(topPoint, Collider.radius + SkinWidth, Player.CollisionMask);
        bool checkBotPoint = Physics.CheckSphere(botPoint, Collider.radius + SkinWidth, Player.CollisionMask);

        int counter = 0;
        while (checkTopPoint || checkBotPoint)
        {
            // Kolla bot
            Collider[] botOverlaps = Physics.OverlapSphere(botPoint, Collider.radius + SkinWidth, Player.CollisionMask);
            for(int i = 0; i < botOverlaps.Length; i++)
            {
                Vector3 closest = botOverlaps[i].ClosestPoint(botPoint);
                Vector3 overlapDirection = closest - botPoint;

                //flytta bort från träffen
                PlayerGameObject.transform.position += -overlapDirection.normalized * ((Collider.radius + SkinWidth) - overlapDirection.magnitude);
                Vector3 normalForce = PhysicsFunctions.NormalForce(Velocity, -overlapDirection.normalized);
                Velocity += normalForce;
                Velocity = Friktion(Velocity, normalForce);

                topPoint = PlayerGameObject.transform.position + Vector3.up * (Collider.height - Collider.radius); // + höjden av collider - radius
                botPoint = PlayerGameObject.transform.position + Vector3.up * Collider.radius; // + radius
            }
            // Kolla Top
            Collider[] topOverlaps = Physics.OverlapSphere(topPoint, Collider.radius + SkinWidth, Player.CollisionMask);
            for (int i = 0; i < topOverlaps.Length; i++)
            {
                Vector3 closest = topOverlaps[i].ClosestPoint(topPoint);
                Vector3 overlapDirection = closest - topPoint;

                //flytta bort från träffen
                PlayerGameObject.transform.position += -overlapDirection.normalized * ((Collider.radius + SkinWidth) - overlapDirection.magnitude);
                Vector3 normalForce = PhysicsFunctions.NormalForce(Velocity, -overlapDirection.normalized);
                Velocity += normalForce;

                topPoint = PlayerGameObject.transform.position + Vector3.up * (Collider.height - Collider.radius); // + höjden av collider - radius
                botPoint = PlayerGameObject.transform.position + Vector3.up * Collider.radius; // + radius
            }

            // Kolla igen 
            checkTopPoint = Physics.CheckSphere(topPoint, Collider.radius + SkinWidth, Player.CollisionMask);
            checkBotPoint = Physics.CheckSphere(botPoint, Collider.radius + SkinWidth, Player.CollisionMask);

            if (counter > 50)
                break;

            counter++;
        }
    }

    //Collision
    //Friktion
    //Air resistance?
}
