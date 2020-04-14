using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallScript : MonoBehaviour
{
    [SerializeField] private float speed = 6;
    [SerializeField] private float Gravity = 6;
    [SerializeField] private LayerMask HitMask;
    [SerializeField] private float bossDamage = 20;
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
                CollCast.transform.gameObject.GetComponent<Enemy>().Defeated();
                Debug.Log("Träffar  Fiende");
            }else if(CollCast.transform.gameObject.tag == "Boss")
            {
                CollCast.transform.gameObject.GetComponent<Boss>().TakeDamage(bossDamage);
                Debug.Log("boss hit");
            }

            Destroy(transform.gameObject);
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
