using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseState : State
{
    protected Boss boss;
    protected Boss Boss => boss = boss ?? (Boss)owner;
    protected LayerMask CollisionMask { get { return Boss.GetCollisionMask(); } }
    protected CapsuleCollider Collider { get { return Boss.GetCollider(); } }

    
}
