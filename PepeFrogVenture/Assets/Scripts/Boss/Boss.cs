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
    //private void OnTriggerEnter(Collider other)
    //{
    //    isEncountered = true;
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DestroyableLilypad>())
        {
            other.GetComponent<DestroyableLilypad>().DestroyLilypadNow();
        }
        else if(other.tag == "Player")
        {
            EventSystem.Current.FireEvent(new PlayerHitEvent(other.gameObject, 10));
        }
        else
        {
            //statemachine.TransitionTo<BossReturnToStartPositionState>();
            //skapa ett state för att åka tillbaka till vattnet
        }
    }
}
