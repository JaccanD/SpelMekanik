using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

// Author Valter Fallsterljung
[CreateAssetMenu(menuName = "BossState/RapidAttackingState")]
public class BossRapidAttackingState : BossBaseState
{
    private float currentCool;
    [SerializeField] private float cooldown = 0.3f;
    [SerializeField] private int shoots = 15;
    [SerializeField] private float rotationSpeed = 3;
    [SerializeField] private float shootSpread = 5;
    private int shootsLeftBeforeSubmerge;

    public override void Enter()
    {
        shootsLeftBeforeSubmerge = shoots;
    }
    public override void Run()
    {
        RotateTowardPlayer(Player.transform.position, rotationSpeed);
        attack();
    }
    private void attack()
    {
        currentCool -= Time.deltaTime;

        if (currentCool > 0)
            return;
        EventSystem.Current.FireEvent(new BossRapidAttackEvent());
        Shoot(shootSpread);
        shootsLeftBeforeSubmerge -= 1;
        if (shootsLeftBeforeSubmerge < 1)
        {
            shootsLeftBeforeSubmerge = 5;
            stateMachine.TransitionTo<BossDivingState>();
        }
        currentCool = cooldown;
    }
}
