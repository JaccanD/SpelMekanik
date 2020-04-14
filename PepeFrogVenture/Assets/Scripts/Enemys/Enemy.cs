using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent agent;
    protected StateMachine statemachine;
    public State[] states;
    
    public float speed = 2;
    private int currentPatrolPoint = 0;
    private CapsuleCollider Collider;
    [SerializeField] LayerMask CollisionMask;
    public PlayerKontroller3D player;
    public Vector3[] patrolPoints;
    [SerializeField]private GameController controller;
    [SerializeField] private float damage = 2;
    private void Awake()
    {
        Collider = GetComponent<CapsuleCollider>();
        agent = GetComponent<NavMeshAgent>();
        //lär mig navmesh när jag har tid
    }
    private void Start()
    {
        statemachine = new StateMachine(this, states);
    }

    private void Update()
    {
        statemachine.Run();
    }
    public int getCurrentPatrolPoint()
    {
        return currentPatrolPoint;
    }
    public void setPatrolPoint(int point)
    {
        currentPatrolPoint = point;
    }
    public float getDamage()
    {
        return damage;
    }
    public float GetSpeed()
    {
        return speed;
    }
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    public LayerMask GetCollisionMask()
    {
        return CollisionMask;

    }
    public CapsuleCollider GetCollider()
    {
        return Collider;
    }
    public Transform GetTransform()
    {
        return transform;
    }
    public void Defeated()
    {
        Debug.Log("Kör Defeated");
        statemachine.TransitionTo<EnemyDefeatedState>();
    }
    public GameController getGamecontroller()
    {
        return controller;
    }
    private void OnTriggerEnter(Collider other)
    {
        Defeated();
    }
}
