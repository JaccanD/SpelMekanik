using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/WalkingState")]
public class EnemyWalkingState : EnemyBaseState
{
    //[SerializeField] private Vector3[] patrolPoints;
    [SerializeField] private float spotPlayerDistance;
    private int currentPatrolPoint = 0;

    public override void Enter()
    {
        ChooseClosest();
    }

    public override void Run()
    {
        Enemy.agent.SetDestination(patrolPoints[currentPatrolPoint]);
        //Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, patrolPoints[currentPatrolPoint], speed * Time.deltaTime);
        if (CanSeePlayer() && Vector3.Distance(Enemy.transform.position, Enemy.player.transform.position) < spotPlayerDistance)
            stateMachine.TransitionTo<EnemyChasePlayerState>();
        if (Vector3.Distance(Enemy.transform.position, patrolPoints[currentPatrolPoint]) < 1)
            currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
    }

    private void ChooseClosest()
    {
        int closest = 0;
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            float distance = Vector3.Distance(Enemy.transform.position, patrolPoints[i]);
            if (distance < Vector3.Distance(Enemy.transform.position, patrolPoints[closest]))
                closest = i;
        }
        currentPatrolPoint = closest;
    }
}