﻿using System.Collections;
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
        Enemy.agent.SetDestination(patrulleringspunkter[currentPatrolPoint].transform.position);
        //Enemy.agent.SetDestination(patrolPoints[currentPatrolPoint]);
        if (CanSeePlayer() && Vector3.Distance(Enemy.transform.position, Enemy.player.transform.position) < spotPlayerDistance)
            stateMachine.TransitionTo<EnemyChasePlayerState>();
        //if (Vector3.Distance(Enemy.transform.position, patrolPoints[currentPatrolPoint]) < 1)
        //    currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
        if (Vector3.Distance(Enemy.transform.position, patrulleringspunkter[currentPatrolPoint].transform.position) < 1)
            currentPatrolPoint = (currentPatrolPoint + 1) % patrulleringspunkter.Length;
    }

    //private void ChooseClosest()
    //{
    //    int closest = 0;
    //    for (int i = 0; i < patrulleringspunkter.Length; i++)
    //    {
    //        float distance = Vector3.Distance(Enemy.transform.position, patrolPoints[i]);
    //        if (distance < Vector3.Distance(Enemy.transform.position, patrolPoints[closest]))
    //            closest = i;
    //    }
    //    currentPatrolPoint = closest;
    //}
    private void ChooseClosest()
    {
        int closest = 0;
        for (int i = 0; i < patrulleringspunkter.Length; i++)
        {
            float distance = Vector3.Distance(Enemy.transform.position, patrulleringspunkter[i].transform.position);
            if (distance < Vector3.Distance(Enemy.transform.position, patrulleringspunkter[closest].transform.position))
                closest = i;
        }
        currentPatrolPoint = closest;
    }
}