using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Callback;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent agent;
    protected StateMachine statemachine;
    public State[] states;

    public float speed = 2;
    private CapsuleCollider Collider;
    [SerializeField] LayerMask CollisionMask;
    public PlayerKontroller3D player;
    [SerializeField] private Vector3[] patrolPoints;
    [SerializeField] private GameController controller;
    [SerializeField] private float damage = 2;
    [SerializeField] private float Health = 4;
    private void Awake()
    {
        EventSystem.Current.RegisterListener<EnemyHitEvent>(OnHit);
        Collider = GetComponent<CapsuleCollider>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {

        statemachine = new StateMachine(this, states);
    }

    private void Update()
    {
        statemachine.Run();
    }
    public Vector3[] getPatrolPoints()
    {
        return patrolPoints;
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
        EventSystem.Current.UnRegisterListener<EnemyHitEvent>(OnHit);
    }
    public GameController getGamecontroller()
    {
        return controller;
    }
    public void OnHit(EnemyHitEvent e)
    {
        if (e.EnemyHit != gameObject)
            return;
        Health -= e.Damage;
        if(Health <= 0)
        {

            Defeated();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Defeated();
    }
}
