using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPatrol : MonoBehaviour
{
    [SerializeField] GameObject[] points;
    [SerializeField] float speed;
    [SerializeField] float turnSpeed;
    [SerializeField] int startAtIndex = 0;

    private int currentIndex = 0;
    private Vector3 direction { get { return transform.rotation * Vector3.forward; } }
    private GameObject nextPoint { get { return points[currentIndex]; } }

    private void Start()
    {
        currentIndex = startAtIndex;
    }
    private void Update()
    {
        Rotate();
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
        if(Vector3.Distance(transform.position, nextPoint.transform.position) < 0.5f)
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
    void Rotate()
    {
        Vector3 between = nextPoint.transform.position - transform.position;

        between.y = 0;

        Quaternion rotation = Quaternion.LookRotation(between);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
    }
}
