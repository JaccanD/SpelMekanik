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
        
        if(Timer % 2 == 0)
        {
            Enemy.transform.position += 4 * Vector3.forward * Time.deltaTime;
        }
        if(Timer % 3 == 0)
        {
            Enemy.transform.position += 4 * Vector3.back * Time.deltaTime;
        }
        if(Timer % 4 == 0)
        {
            Enemy.transform.position += 4 * Vector3.right * Time.deltaTime;
        }
        if(Timer % 5 == 0)
        {
            Enemy.transform.position += 4 * Vector3.left * Time.deltaTime;
        }
        if(Timer > 5)
        {
            Despawn();
        }
    }
    private void Despawn()
    {
        Destroy(Enemy.transform.gameObject);
    }
}
