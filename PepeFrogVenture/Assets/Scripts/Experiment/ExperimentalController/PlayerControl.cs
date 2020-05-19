using System.Collections;
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
        int a = 0;
        EventSystem.Current.RegisterListener(typeof(HookHitEvent), Pull);
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

}
