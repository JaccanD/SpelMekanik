using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "PlayerState/DeadState")]
public class PlayerDeadState : PlayerBaseState
{
    private GameObject RespawnPoint;
    private float Timer = 0;

    public override void Enter()
    {
        RespawnPoint = Controller.CurrentRespawnPoint;
        Velocity = Vector3.zero;
    }
    public override void Run()
    {
        Timer += Time.deltaTime;
        if (Timer >= 1)
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
        Timer = 0;
        stateMachine.TransitionTo<PlayerStandingState>();
        
    }
}
