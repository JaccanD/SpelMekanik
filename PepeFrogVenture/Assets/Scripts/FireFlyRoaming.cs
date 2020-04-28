using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlyRoaming : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 moveToPosition;
    private Vector3 lastPosition;
    private new SphereCollider collider;
    [SerializeField] private float speed = 2;
    [SerializeField] private float radius = 2;
    [SerializeField] LayerMask collisionMask;
    
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<SphereCollider>();
        startPosition = transform.position;
        lastPosition = transform.position;
        moveToPosition = Random.insideUnitSphere * radius + startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 nextFrameMovement = Vector3.MoveTowards(transform.position, moveToPosition, speed * Time.deltaTime);
        Vector3 castDistance = nextFrameMovement - transform.position;
        RaycastHit cast;
        bool hit = Physics.SphereCast(transform.position, collider.radius, moveToPosition.normalized, out cast, Vector3.Distance(nextFrameMovement, transform.position), collisionMask);
        if (!hit)
        {
            transform.position = nextFrameMovement;
            if (Vector3.Distance(transform.position, moveToPosition) < 0.2f)
            {
                moveToPosition = Random.insideUnitSphere * radius + startPosition;
                lastPosition = transform.position;
            }
        }
        else
        {
            Debug.Log("Fly hit something");
            moveToPosition = Random.insideUnitSphere * radius + startPosition;
            lastPosition = transform.position;
        }
            
    }
}
