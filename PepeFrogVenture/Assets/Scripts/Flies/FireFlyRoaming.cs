using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Author: Valter Falsterljung
public class FireFlyRoaming : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 moveToPosition;
    private Vector3 lastPosition;
    private new SphereCollider collider;
    [SerializeField] private float speed = 2;
    [SerializeField] private float radius = 2;
    [SerializeField] LayerMask collisionMask;
    [SerializeField] private float skinWidth = 0.1f;

    void Start()
    {
        collider = GetComponent<SphereCollider>();
    }
    private void OnEnable()
    {
        startPosition = transform.position;
        lastPosition = transform.position;
        moveToPosition = Random.insideUnitSphere * radius + startPosition;
    }

    void Update()
    {
        MoveFly();
        Rotate();
    }
    public void SetStartingPositions()
    {
        startPosition = transform.position;
        lastPosition = transform.position;
        moveToPosition = Random.insideUnitSphere * radius + startPosition;
    }
    private void MoveFly()
    {
        Vector3 nextFrameMovement = Vector3.MoveTowards(transform.position, moveToPosition, speed * Time.deltaTime);

        if (CollisionDetection(nextFrameMovement) || Vector3.Distance(transform.position, moveToPosition) < skinWidth)
        {
            NewMoveToPosition();
        }
        else
        {
            transform.position = nextFrameMovement;
        }

    }
    private bool CollisionDetection(Vector3 movement)
    {
        Vector3 castDistance = movement - transform.position;
        RaycastHit cast;
        return(Physics.SphereCast(transform.position, collider.radius, castDistance.normalized, out cast, castDistance.magnitude, collisionMask));
    }
    private void Rotate()
    {
        Quaternion rotation = Quaternion.LookRotation(moveToPosition - lastPosition) * Quaternion.Euler(0,69,0).normalized;
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);
    }
    private void NewMoveToPosition()
    {
        moveToPosition = Random.insideUnitSphere * radius + startPosition;
        lastPosition = transform.position;
    }
}