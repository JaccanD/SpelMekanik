using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class BossProjectile : MonoBehaviour
{
    //private float speed;
    //[SerializeField] private float projectileSpeed;
    //public Vector3 target;
    //private Vector3 start;
    //[SerializeField] private float projectileHeight;
    [SerializeField] private float projektileForce = 1000f;
    [SerializeField] private float damage = 4;

    private void Start()
    {
        //start = transform.position;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * projektileForce);
        Destroy(gameObject, 5f);
    }

    //public void setTarget(Vector3 targetVector)
    //{
    //    target = targetVector;
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EventSystem.Current.FireEvent(new PlayerHitEvent(collision.gameObject, damage));
            //collision.gameObject.GetComponent<PlayerKontroller3D>().GetGameController().TakeDamage(damage);
            Debug.Log("player toke damage");
        }
        Destroy(gameObject);
    }
    //gamla sättet att flytta projektilen på
    //void Update()
    //{
    //    speed += Time.deltaTime;

    //    speed = speed % 5f;

    //    transform.position = MathParabola.Parabola(start, target, projectileHeight, speed / projectileSpeed);
    //    if(Vector3.Distance(transform.position,target) < 0.1f)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

}
