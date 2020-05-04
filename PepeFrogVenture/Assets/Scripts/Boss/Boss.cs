using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

// Author: Valter Falsterljung
public class Boss : Enemy
{
    public List<DestroyableLilypad> lilypads;
    private bool isInvulnarable;
    private bool isEncountered;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject shootPoint;
    private void Awake()
    {
        EventSystem.Current.RegisterListener(typeof(EnemyHitEvent), TakeDamage);
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
        return Health;
    }
    public void TakeDamage(Callback.Event eb)
    {
        EnemyHitEvent e = (EnemyHitEvent)eb;
        Debug.Log("BossDamageTaken");
        Health -= e.Damage;
        Debug.Log(Health);
        if(Health <= 0)
            statemachine.TransitionTo<BossDefeatedState>();
    }
    /*public void OnHit(EnemyHitEvent e)
    {
        Debug.Log("BossDamageTaken");
        health -= e.Damage;
        Debug.Log(health);
        if (health <= 0)
            statemachine.TransitionTo<BossDefeatedState>();
    }*/
    private void OnTriggerEnter(Collider other)
    {
        isEncountered = true;
    }
}
