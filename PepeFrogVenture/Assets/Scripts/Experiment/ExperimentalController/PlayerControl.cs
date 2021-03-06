﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class PlayerControl : MonoBehaviour
{
    // En state Machine
    [SerializeField] private State[] states;
    private StateMachine stateMachine;

    //Movement
    // SkinWidth, CollisionMask, GroundCheckDistance
    private Vector3 velocity = Vector3.zero;
    private Vector3 point;
    private CapsuleCollider coll;
    private float stunnedTime = 0;
    private bool isStunned = false;

    [Header("Movement Settings")]
    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private float skinWidth;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float gravity = 9.82f;
    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;
    [Range(0.01f, 0.99f)] [SerializeField] private float staticFriction;
    [Range(0.01f, 0.99f)] [SerializeField] private float dynamicFriction;
    [Range (0.01f, 0.99f)][SerializeField] private float airResistance;

    public Vector3 Velocity { get; set; }
    public Vector3 Point { get { return point; } }
    public GameObject GameObject { get { return gameObject; } }
    public CapsuleCollider Collider { get { return coll; } }
    public LayerMask CollisionMask { get { return collisionMask; } }
    public State InState { get { return stateMachine.GetCurrentState(); } }

    public float SkinWidth {  get { return skinWidth; } }
    public float StaticFriction { get { return staticFriction; } }
    public float DynamicFriction { get { return dynamicFriction; } }
    public float Gravity { get { return gravity; } }
    public float Acceleration { get { return acceleration; } }
    public float GroundCheckDistance { get { return groundCheckDistance; } }
    public float AirResistance { get { return airResistance; } }
    public float MaxSpeed { get { return maxSpeed; } }

    private void Awake()
    {
        coll = GetComponent<CapsuleCollider>();
        stateMachine = new StateMachine(this, states);

        EventSystem.Current.RegisterListener(typeof(PlayerDeathEvent), Die);
        EventSystem.Current.RegisterListener(typeof(HookHitEvent), Pull);
        EventSystem.Current.RegisterListener(typeof(Pushed), IsPushed);
        EventSystem.Current.RegisterListener(typeof(PlayerRespawnEvent), PlayerRespawned);
    }
    private void Update()
    {
            stateMachine.Run();
    }

    public void Pull(Callback.Event eb)
    {
        HookHitEvent e = (HookHitEvent)eb;
        point = e.Point;
        stateMachine.TransitionTo<PlayerControlSwingState>();
    }

    //TODO
    // Lyssnare för HitEvent, DeathEvent, RespawnEvent
    // Ta från gammla controller

    private void IsPushed(Callback.Event eb)
    {
        Pushed e = (Pushed)eb;
        Vector3 direction = (e.Player.transform.position - e.Origin).normalized;
        Velocity = direction * e.PushBackStrenght + (Vector3.up * e.Height);
        Stun(e.StunDuration);
    }
    private void Stun(float duration)
    {
        stunnedTime = duration;
        isStunned = true;
        StartCoroutine(StunCountDOwn());

    }
    private void PlayerRespawned(Callback.Event eb)
    {
        stateMachine.TransitionTo<PlayerControlIdleState>();
    }
    IEnumerator StunCountDOwn()
    {
        yield return new WaitForSeconds(stunnedTime);

        isStunned = false;
    }

    public void Die(Callback.Event eb)
    {
        PlayerDeathEvent e = (PlayerDeathEvent)eb;
        stateMachine.TransitionTo<PlayerControlDeadState>();
    }
}
