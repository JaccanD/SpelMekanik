using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Author: Jacob Didenbäck
public class Tounge : MonoBehaviour
{

    private StateMachine stateMachine;
    public State[] states;

    [SerializeField] private float Speed = 5;
    [SerializeField] private LayerMask ToungeMask;
    [SerializeField] private float MaxDistance;
    [SerializeField] private Material toungeMaterial;
    private Transform mouth;
    private GameObject Player;
    private SphereCollider Coll;
    private GameObject Cylinder;
    private Vector3 Point;

    public Transform Mouth { get { return mouth; } set { mouth = value; } }
    public Material ToungeMaterial { get { return toungeMaterial; } }
    public float GetMaxDistance()
    {
        return MaxDistance;
    }
    public float GetSpeed()
    {
        return Speed;
    }
    public LayerMask GetToungeMask()
    {
        return ToungeMask;
    }
    public GameObject GetPlayer()
    {
        return Player;
    }
    public SphereCollider GetColl()
    {
        return Coll;
    }
    public GameObject GetGameObject()
    {
        return gameObject;
    }
    public GameObject GetCylinder()
    {
        return Cylinder;
    }
    public void SetCylinder(GameObject newCyl)
    {
        Cylinder = newCyl;
    }
    public Vector3 GetPoint()
    {
        return Point;
    }
    public void SetPoint(Vector3 newPoint)
    {
        Point = newPoint;
    }
    private void Awake()
    {
        stateMachine = new StateMachine(this, states);
        Coll = GetComponentInChildren<SphereCollider>();
        Player = GameObject.FindGameObjectWithTag("Player");

        
    }

    private void Update()
    {
        stateMachine.Run();
    }


}
