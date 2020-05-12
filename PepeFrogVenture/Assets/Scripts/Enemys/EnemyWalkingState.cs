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
        Enemy.agent.SetDestination(PatrolPoints[currentPatrolPoint].transform.position);

        if (CanSeePlayer() && Vector3.Distance(Enemy.transform.position, Enemy.player.transform.position) < spotPlayerDistance)
            stateMachine.TransitionTo<EnemyChasePlayerState>();

        if (Vector3.Distance(Enemy.transform.position, PatrolPoints[currentPatrolPoint].transform.position) < 1)
            currentPatrolPoint = (currentPatrolPoint + 1) % PatrolPoints.Length;
    }


    private void ChooseClosest()
    {
        int closest = 0;
        for (int i = 0; i < PatrolPoints.Length; i++)
        {
            float distance = Vector3.Distance(Enemy.transform.position, PatrolPoints[i].transform.position);
            if (distance < Vector3.Distance(Enemy.transform.position, PatrolPoints[closest].transform.position))
                closest = i;
        }
        currentPatrolPoint = closest;
    }
}