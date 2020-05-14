using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Valter Falsterljung
public class BossBaseState : State
{
    protected Boss boss;
    protected Boss Boss => boss = boss ?? (Boss)owner;
    protected LayerMask CollisionMask { get { return Boss.GetCollisionMask(); } }
    //protected CapsuleCollider Collider { get { return Boss.GetCollider(); } }
    //protected BoxCollider Collider { get { return Boss.getCollider(); } }
    protected PlayerKontroller3D Player { get { return Boss.GetPlayer(); } }
    protected Vector3 Position { get { return Boss.transform.position; } set { Boss.transform.position = value; } }
    protected GameObject[] superJumpPoints { get { return Boss.GetSuperJumpPoints(); } }

    
}
