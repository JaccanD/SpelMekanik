using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerControlBaseState : State
{
    protected PlayerControl player;
    protected PlayerControl Player => player = player ?? (PlayerControl)owner;

    protected Vector3 Velocity { get { return Player.Velocity; } set { Player.Velocity = value; } }
    protected GameObject PlayerGameObject { get { return Player.GameObject; } }

    public override void Run()
    {
        
    }
    protected void MovePlayer()
    {
        PlayerGameObject.transform.position += Velocity * Time.deltaTime;
    }

    // Inte säker på om den ska returnera nästa move eller inte
    private void CheckCollision()
    {

    }

    //Collision
    //Friktion
    //Air resistance?
}
