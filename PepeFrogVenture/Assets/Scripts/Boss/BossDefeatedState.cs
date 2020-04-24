using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

[CreateAssetMenu(menuName = "BossState/DefeatedState")]
public class BossDefeatedState : BossBaseState
{
    public override void Enter()
    {
        Debug.Log("Bossdead");
        Die();
    }
    private void Die()
    {
        //dödsanimation och skit
        EventSystem.Current.FireEvent(new BossDeadEvent());
        Destroy(Boss.gameObject);
    }
}
