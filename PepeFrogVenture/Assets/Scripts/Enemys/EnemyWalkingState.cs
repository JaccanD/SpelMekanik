using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/WalkingState")]
// Author: Valter Falsterljung
public class EnemyWalkingState : EnemyBaseState
{
    [SerializeField] private float spotPlayerDistance;
    private int currentPatrolPoint = 0;

    public override void Enter()
    {
        ChooseClosest();
    }

    public override void Run()
    {
        Enemy.agent.SetDestination(patrolPoints[currentPatrolPoint].transform.position);

        if (CanSeePlayer() && Vector3.Distance(Enemy.transform.position, Enemy.player.transform.position) < spotPlayerDistance)
            stateMachine.TransitionTo<EnemyChasePlayerState>();

        if (Vector3.Distance(Enemy.transform.position, patrolPoints[currentPatrolPoint].transform.position) < 1)
            currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
    }


    private void ChooseClosest()
    {
        int closest = 0;
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            float distance = Vector3.Distance(Enemy.transform.position, patrolPoints[i].transform.position);
            if (distance < Vector3.Distance(Enemy.transform.position, patrolPoints[closest].transform.position))
                closest = i;
        }
        currentPatrolPoint = closest;
    }
}