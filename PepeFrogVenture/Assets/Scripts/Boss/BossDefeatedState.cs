using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

[CreateAssetMenu(menuName = "BossState/DefeatedState")]
// Author: Valter Fallsterljung
public class BossDefeatedState : BossBaseState
{
    private float currentTime = 0;
    private bool hasDied;
    [SerializeField] private float timeUntilDeathEvent = 5f;

    public override void Run()
    {
        currentTime += Time.deltaTime;
        if(currentTime > timeUntilDeathEvent && hasDied == false)
        {
            Die();
            hasDied = true;
        }
    }
    private void Die()
    {
        EventSystem.Current.FireEvent(new BossDeadEvent());
    }
}
