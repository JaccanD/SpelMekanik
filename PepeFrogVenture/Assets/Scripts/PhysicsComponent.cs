using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsComponent : MonoBehaviour 
{
    private float Speed = 2.0f;
    private Vector3 direction = new Vector3(1, 0, 0);

    private float distance = 0;
    void Start()
    {
        
    }
    public Vector2 GetVelocity()
    {
        return direction.normalized* Speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 nextMove = direction * Speed * Time.deltaTime;
        transform.position += nextMove;
        distance += nextMove.magnitude * direction.x;
        if(distance >= 3)
        {
            direction *= -1;
        }
        if(distance <= 0)
        {
            direction *= -1;
        }

    }
}
