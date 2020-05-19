using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

[CreateAssetMenu(menuName = "BossState/DefeatedState")]
// Author: Valter Fallsterljung
public class BossDefeatedState : BossBaseState
{
    public override void Enter()
    {
        Die();
    }
    private void Die()
    {
        EventSystem.Current.FireEvent(new BossDeadEvent());
        Destroy(Boss.gameObject);
    }
}
