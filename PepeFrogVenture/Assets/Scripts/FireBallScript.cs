using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;
// Author: Jacob Didenbäck
public class FireBallScript : MonoBehaviour
{
    [SerializeField] private float speed = 6;
    [SerializeField] private float Gravity = 6;
    [SerializeField] private LayerMask HitMask;
    [SerializeField] private float Damage = 20;
    [SerializeField] private ParticleSystem enemyBurned;
    private Vector3 Velocity = Vector3.zero;
    private SphereCollider Coll;


    // Update is called once per frame
    void Update()
    {
        Velocity += Gravity * Vector3.down * Time.deltaTime;
        transform.position += Velocity * Time.deltaTime;
        Collider[] colls = Physics.OverlapSphere(transform.position + Coll.center, Coll.radius, HitMask);
        if (colls.Length > 0)
        {
            for (int i = 0; i < colls.Length; i++)
            {
                if (colls[i].transform.gameObject.tag == "Enemy")
                {
                    GameObject.Instantiate(enemyBurned, transform.position, transform.rotation);
                    EventSystem.Current.FireEvent(new EnemyHitEvent(colls[i].transform.gameObject, Damage));
                }
                Destroy(gameObject);
            }
        }
    }
    private void Awake()
    {
        Velocity = speed * (transform.rotation * Vector3.forward);
        Coll = GetComponent<SphereCollider>();
        Invoke("TimeOut", 6);
    }
    private void TimeOut()
    {
        Destroy(transform.gameObject);
    }
}
