using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/DefeatedState")]
public class EnemyDefeatedState : EnemyBaseState
{
    private float Timer = 0;
    public override void Enter()
    {
        Debug.Log("ÄR i defeatedState");
    }
    public override void Run()
    {

        Timer += 1 * Time.deltaTime;
        
        if(Timer > 1)
        {
            Despawn();
        }
    }
    private void Despawn()
    {
        Destroy(Enemy.transform.gameObject);
    }
}
