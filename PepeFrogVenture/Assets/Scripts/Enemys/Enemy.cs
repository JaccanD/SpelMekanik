﻿using System.Collections;
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
    //private CapsuleCollider Collider;
    private BoxCollider collider;
    [SerializeField] LayerMask CollisionMask;
    public PlayerKontroller3D player;
    [SerializeField] private Vector3[] patrolPoints;
    [SerializeField] private GameObject[] patrulleringpunkter;
    [SerializeField] private GameController controller;
    [SerializeField] protected float damage = 2;
    [SerializeField] protected float Health = 4;
    private void Awake()
    {
        GameObject PlayerGo = GameObject.FindGameObjectWithTag("Player");
        player = PlayerGo.GetComponent<PlayerKontroller3D>();
        EventSystem.Current.RegisterListener(typeof(EnemyHitEvent),OnHit);
        collider = GetComponent<BoxCollider>();
        //Collider = GetComponent<CapsuleCollider>();
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
    public GameObject[] getPatrulleringsPunkter()
    {
        return patrulleringpunkter;
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
    public BoxCollider getCollider()
    {
        return collider;
    }
    //public CapsuleCollider GetCollider()
    //{
    //    return Collider;
    //}
    public Transform GetTransform()
    {
        return transform;
    }
    public void Defeated()
    {
        Debug.Log("Kör Defeated");
        statemachine.TransitionTo<EnemyDefeatedState>();
        EventSystem.Current.UnRegisterListener(typeof(EnemyHitEvent), OnHit);
    }
    public GameController getGamecontroller()
    {
        return controller;
    }
    public void OnHit(Callback.Event eb)
    {
        Debug.Log("ENEMY HIT");
        EnemyHitEvent e = (EnemyHitEvent)eb;
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
        if(other.tag == "Player")
        {
            //Defeated();
            EventSystem.Current.FireEvent(new EnemyHitEvent(this.gameObject, Health));
        }

    }
}
