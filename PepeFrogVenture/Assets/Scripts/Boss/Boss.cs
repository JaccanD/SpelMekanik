using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class Boss : Enemy
{
    public List<DestroyableLilypad> lilypads;
    private bool isInvulnarable;
    private float health = 20;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject shootPoint;

    private void Awake()
    {
           
    }
    private void SinkAPad(Lilypads pad)
    {
        pad.setIsSInking(true);
    }

    public GameObject getProjectile()
    {
        return projectile;
    }
    public GameObject getShootPoint()
    {
        return shootPoint;
    }
    public float getHealth()
    {
        return health;
    }
    public void TakeDamage(float value)
    {
        Debug.Log("BossDamageTaken");
        health -= value;
        Debug.Log(health);
        if(health <= 0)
            statemachine.TransitionTo<BossDefeatedState>();
    }
    public void OnHit(EnemyHitEvent e)
    {
        Debug.Log("BossDamageTaken");
        health -= e.Damage;
        Debug.Log(health);
        if (health <= 0)
            statemachine.TransitionTo<BossDefeatedState>();
    }
}
