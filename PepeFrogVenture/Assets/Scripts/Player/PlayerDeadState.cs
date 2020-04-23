using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/DeadState")]
public class PlayerDeadState : PlayerBaseState
{
    private GameObject RespawnPoint;

    public override void Enter()
    {
        RespawnPoint = Controller.CurrentRespawnPoint;
        Velocity = Vector3.zero;
    }
    public override void Run()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Respawn();
        }
    }

    public override void Exit()
    {
    }
    private void Respawn()
    {
        transform.position = RespawnPoint.transform.position;
        stateMachine.TransitionTo<PlayerStandingState>();
    }
}
