using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPatrol : MonoBehaviour
{
    [SerializeField] GameObject[] points;
    [SerializeField] float speed;
    [SerializeField] float turnSpeed;

    private int currentIndex = 0;
    private Vector3 direction { get { return transform.rotation * Vector3.forward; } }
    private GameObject nextPoint { get { return points[currentIndex]; } }

    private void Update()
    {
        MoveTowardsNextPoint();
    }

    void MoveTowardsNextPoint()
    {
        if (!NextPointReached())
        {
            
            transform.position += speed * Time.deltaTime * direction;
        }
    }
    bool NextPointReached()
    {
        if(Vector3.Distance(transform.position, nextPoint.transform.position) < 0.1f)
        {
            IncrementIndex();
            return true;
        }
        return false;
    }
    void IncrementIndex()
    {
        currentIndex++;
        currentIndex = currentIndex % points.Length;
    }
}
