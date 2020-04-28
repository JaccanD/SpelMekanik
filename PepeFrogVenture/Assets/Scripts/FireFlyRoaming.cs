using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlyRoaming : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 moveToPosition;
    private Vector3 lastPosition;
    [SerializeField] private float speed = 2;
    [SerializeField] private float radius = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        lastPosition = transform.position;
        moveToPosition = Random.insideUnitSphere * radius + startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveToPosition, speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, moveToPosition) < 0.2f)
        {
            Debug.Log("changing position");
            moveToPosition = Random.insideUnitSphere * radius + startPosition;
            lastPosition = transform.position;
        }
    }
}
