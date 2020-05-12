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

    public override void Enter()
    {
        Enemy.agent.speed = Speed;
    }

    protected bool CanSeePlayer()
    {
        return !Physics.Linecast(Enemy.transform.position, Enemy.player.transform.position, Enemy.GetCollisionMask());
    }
}
