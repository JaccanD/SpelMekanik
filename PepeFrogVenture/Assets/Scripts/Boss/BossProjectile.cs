using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

// Author: Valter Fallsterljung
public class BossProjectile : MonoBehaviour
{
    private float projectileForce;
    private float projectileDamage = 3;
    private float currentLifeTime;

    [SerializeField] private ParticleSystem bossProjectileSplash;
    [SerializeField] private float maxLifeTime = 5;

    private void OnEnable()
    {
        currentLifeTime = 0;
    }
    private void Update()
    {
        currentLifeTime += Time.deltaTime;
        if(currentLifeTime > maxLifeTime)
        {
            DisableThisObject();
        }
    }
    public void SetDamage(float damage)
    {
        projectileDamage = damage;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(bossProjectileSplash, transform.position, transform.rotation);
            EventSystem.Current.FireEvent(new PlayerHitEvent(collision.gameObject, projectileDamage));
        }
        DisableThisObject();
    }
    private void DisableThisObject()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
