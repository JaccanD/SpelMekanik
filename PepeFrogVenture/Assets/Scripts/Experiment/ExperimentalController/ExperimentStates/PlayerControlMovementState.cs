using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerControlState/MovementState")]
public class PlayerControlMovementState : PlayerControlBaseState
{
    public override void Run()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Velocity = direction;

        MovePlayer();
    }
}
