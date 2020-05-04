using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/DefeatedState")]
// Author: Valter Falsterljung
public class EnemyDefeatedState : EnemyBaseState
{
    private float Timer = 0;
    [SerializeField] private float timeToDie = 1;

    public override void Run()
    {

        Timer += 1 * Time.deltaTime;
        
        if(Timer > timeToDie)
        {
            Despawn();
        }
    }
    private void Despawn()
    {
        Destroy(Enemy.transform.gameObject);
    }
}
