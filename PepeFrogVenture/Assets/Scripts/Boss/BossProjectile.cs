using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

// Author: Valter Fallsterljung
public class BossProjectile : MonoBehaviour
{
    private float projectileForce;
    private float projectileDamage = 3;
    [SerializeField] private ParticleSystem bossProjectileSplash;

    private void Start()
    {
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
            GameObject.Instantiate(bossProjectileSplash, transform.position, transform.rotation);
            EventSystem.Current.FireEvent(new PlayerHitEvent(collision.gameObject, projectileDamage));
        }
        Destroy(gameObject);
    }
}
