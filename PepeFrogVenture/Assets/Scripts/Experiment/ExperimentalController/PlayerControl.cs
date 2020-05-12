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

    [Header ("Movement Settings")]
    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private float skinWidth;
    [SerializeField] private float groundCheckDistance;
    
    public Vector3 Velocity { get; set; }
    public GameObject GameObject { get { return gameObject; } }

    private void Awake()
    {
        stateMachine = new StateMachine(this, states);
    }
    private void Update()
    {
        stateMachine.Run();
    }

    // Get input för movement
    // 
}
