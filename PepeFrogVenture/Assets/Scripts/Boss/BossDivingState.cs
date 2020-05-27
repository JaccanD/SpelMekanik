﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

[CreateAssetMenu(menuName = "BossState/DivingState")]
// Author: Valter Fallsterljung
public class BossDivingState : BossBaseState
{
    private Lilypads sinkTarget;
    [SerializeField] private float sinkSpeed = 1.5f;
    [SerializeField] private float rotationSpeed = 3;
    [SerializeField] private float threshold = 5f;

    public override void Run()
    {
        RotateTowardPlayer(Player.transform.position, rotationSpeed);
        EventSystem.Current.FireEvent(new BossDivingEvent());
        DiveDown();
        if(Boss.transform.position.y < threshold)
        {
            stateMachine.TransitionTo<BossEmergingState>();
        }
    }
    private void DiveDown()
    {
        Boss.transform.position += Vector3.down * sinkSpeed * Time.deltaTime;
    }
}
