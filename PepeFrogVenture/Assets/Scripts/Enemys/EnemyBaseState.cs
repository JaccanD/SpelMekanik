using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Valter Falsterljung
public class EnemyBaseState : State
{
    protected Enemy enemy;
    protected Enemy Enemy => enemy = enemy ?? (Enemy)owner;
    protected float Speed { get { return Enemy.GetSpeed(); } }
    protected LayerMask CollisionMask { get { return Enemy.GetCollisionMask(); } }
    protected BoxCollider Collider { get { return Enemy.getCollider(); } }
    protected float Damage { get { return Enemy.getDamage(); } }
    protected GameObject[] PatrolPoints { get { return Enemy.getPatrolPoints(); } }
    protected Vector3 Position { get { return Enemy.transform.position; } }
    protected float spotPlayerDistance { get { return Enemy.GetSpotPlayerDistance(); } }
    protected float LostPlayerDistance { get { return Enemy.GetLostPlayerDistance(); } }
    protected int enemiesWhoSeeThePlayer = 0;
    protected bool seesPlayer;

    public override void Enter()
    {
        Enemy.agent.speed = Speed;
    }

    protected bool CanSeePlayer()
    {
        return !Physics.Linecast(Position, Enemy.player.transform.position, CollisionMask);
    }
}
