using Callback;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BossState/StartingState")]
public class BossStartingState : BossBaseState
{
    public override void Enter()
    {
        EventSystem.Current.RegisterListener(typeof(BossBattleStartingEvent), StartEncounter);
    }
    public void StartEncounter(Callback.Event e)
    {
        stateMachine.TransitionTo<BossDivingState>();
    }

}
