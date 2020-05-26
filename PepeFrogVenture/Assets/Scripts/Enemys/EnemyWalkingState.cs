using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

[CreateAssetMenu(menuName = "EnemyState/WalkingState")]
// Author: Valter Fallsterljung
public class EnemyWalkingState : EnemyBaseState
{
    //[SerializeField] private float spotPlayerDistance;
    [SerializeField] private float NearbyEnemyHeardDistance = 5;

    private int currentPatrolPoint = 0;

    public override void Enter()
    {
        ChooseClosest();
        EventSystem.Current.RegisterListener(typeof(PlayerSeenEvent), PlayerSeen);
    }
    public override void Run()
    {
        Enemy.agent.SetDestination(PatrolPoints[currentPatrolPoint].transform.position);

        if (CanSeePlayer() && Vector3.Distance(Enemy.transform.position, Enemy.player.transform.position) < spotPlayerDistance)
        {
            EventSystem.Current.FireEvent(new PlayerSeenEvent(Position));
            stateMachine.TransitionTo<EnemyChasePlayerState>();
        }
        if (Vector3.Distance(Enemy.transform.position, PatrolPoints[currentPatrolPoint].transform.position) < 1)
        {
            currentPatrolPoint = (currentPatrolPoint + 1) % PatrolPoints.Length;
        }
    }
    private void PlayerSeen(Callback.Event eb)
    {
        PlayerSeenEvent e = (PlayerSeenEvent)eb;
        if (Vector3.Distance(Position, e.EnemyPosition) < NearbyEnemyHeardDistance)
        {
            stateMachine.TransitionTo<EnemyPlayerNearState>();
        }

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
    public override void Exit()
    {
        EventSystem.Current.UnRegisterListener(typeof(PlayerSeenEvent), PlayerSeen);
    }
}