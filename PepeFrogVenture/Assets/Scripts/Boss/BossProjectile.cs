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
    private float projectileForce;
    private float projectileDamage = 3;

    private void Start()
    {
        //gamla sättet att ge projektilerna kraft, nu får de kraft av bossen
        //start = transform.position;
        //Rigidbody rb = GetComponent<Rigidbody>();
        //rb.AddForce(transform.forward * projectileForce);
        Destroy(gameObject, 5f);
    }
    public void SetDamage(float damage)
    {
        projectileDamage = damage;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EventSystem.Current.FireEvent(new PlayerHitEvent(collision.gameObject, projectileDamage));
            //collision.gameObject.GetComponent<PlayerKontroller3D>().GetGameController().TakeDamage(damage);
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
