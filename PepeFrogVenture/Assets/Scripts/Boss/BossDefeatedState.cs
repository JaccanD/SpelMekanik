using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Destroy(Boss.gameObject);
    }
}
