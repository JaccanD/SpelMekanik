using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : State
{
    protected Enemy enemy;
    protected Enemy Enemy => enemy = enemy ?? (Enemy)owner;
    protected float speed { get { return Enemy.GetSpeed(); } }
    protected LayerMask CollisionMask { get { return Enemy.GetCollisionMask(); } }
    protected CapsuleCollider Collider { get { return Enemy.GetCollider(); } }
    protected float damage { get { return Enemy.getDamage(); } }
    protected Vector3[] patrolPoints { get { return Enemy.getPatrolPoints(); } }
    

    public override void Enter()
    {
        Enemy.agent.speed = speed;
    }

    protected bool CanSeePlayer()
    {
        return !Physics.Linecast(Enemy.transform.position, Enemy.player.transform.position, Enemy.GetCollisionMask());
    }
}
