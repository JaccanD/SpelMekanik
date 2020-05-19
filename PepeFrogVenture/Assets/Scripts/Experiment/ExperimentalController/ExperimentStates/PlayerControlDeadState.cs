using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerControlState/DeadState")]
public class PlayerControlDeadState : PlayerControlBaseState
{
    public override void Enter()
    {
        Velocity = Vector3.zero;
    }
}
