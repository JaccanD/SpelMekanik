using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseState : State
{
    protected Boss boss;
    protected Boss Boss => boss = boss ?? (Boss)owner;
    protected LayerMask CollisionMask { get { return Boss.GetCollisionMask(); } }
    //protected CapsuleCollider Collider { get { return Boss.GetCollider(); } }
    protected BoxCollider Collider { get { return Boss.getCollider(); } }
    protected PlayerKontroller3D player { get { return Boss.player; } }

    
}
