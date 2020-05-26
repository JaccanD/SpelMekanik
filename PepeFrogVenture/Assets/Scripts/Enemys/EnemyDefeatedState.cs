using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

[CreateAssetMenu(menuName = "EnemyState/DefeatedState")]
// Author: Valter Falsterljung
public class EnemyDefeatedState : EnemyBaseState
{
    private float timer = 0;
    [SerializeField] private float timeToDie = 1;

    public override void Enter()
    {
        EventSystem.Current.FireEvent(new EnemyDeathEvent(Enemy));
        Debug.Log("dead");
    }

    public override void Run()
    {

        timer += 1 * Time.deltaTime;
        
        if(timer > timeToDie)
        {
            Despawn();
        }
    }
    private void Despawn()
    {
        Destroy(Enemy.transform.gameObject);
    }
}
