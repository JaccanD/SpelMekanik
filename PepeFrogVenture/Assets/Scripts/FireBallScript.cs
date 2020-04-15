using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class FireBallScript : MonoBehaviour
{
    [SerializeField] private float speed = 6;
    [SerializeField] private float Gravity = 6;
    [SerializeField] private LayerMask HitMask;
    [SerializeField] private float Damage = 20;
    private Vector3 Velocity = Vector3.zero;
    private SphereCollider Coll;


    // Update is called once per frame
    void Update()
    {
        Velocity += Gravity * Vector3.down * Time.deltaTime;

        transform.position += Velocity * Time.deltaTime;
        bool collisionHit = Physics.SphereCast(transform.position, Coll.radius, Velocity.normalized, out RaycastHit CollCast, Velocity.magnitude * Time.deltaTime, HitMask);
        if (collisionHit)
        {
            if(CollCast.transform.gameObject.tag == "Enemy")
            {
                EventSystem.Current.FireEvent(new EnemyHitEvent(CollCast.transform.gameObject, Damage));
            }else if(CollCast.transform.gameObject.tag == "Boss")
            {
                CollCast.transform.gameObject.GetComponent<Boss>().TakeDamage(Damage);
                Debug.Log("boss hit");
            }

            Destroy(gameObject);
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
