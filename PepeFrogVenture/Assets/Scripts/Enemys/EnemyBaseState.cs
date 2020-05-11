using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Valter Falsterljung
public class EnemyBaseState : State
{
    protected Enemy enemy;
    protected Enemy Enemy => enemy = enemy ?? (Enemy)owner;
    protected float speed { get { return Enemy.GetSpeed(); } }
    protected LayerMask CollisionMask { get { return Enemy.GetCollisionMask(); } }
    protected BoxCollider collider { get { return Enemy.getCollider(); } }
    protected float damage { get { return Enemy.getDamage(); } }
    protected GameObject[] patrolPoints { get { return Enemy.getPatrolPoints(); } }
    protected Vector3 position { get { return Enemy.transform.position; } }

    public override void Enter()
    {
        Enemy.agent.speed = speed;
    }

    protected bool CanSeePlayer()
    {
        return !Physics.Linecast(Enemy.transform.position, Enemy.player.transform.position, Enemy.GetCollisionMask());
    }
}
