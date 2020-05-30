using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

// Author: Valter Fallsterljung
public class Boss : MonoBehaviour
{
    protected StateMachine statemachine;
    public State[] states;
    public List<DestroyableLilypad> lilypads;
    private bool isInvulnarable;
    private bool isEncountered;
    private Vector3 startPosition;

    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private PlayerControl player;
    [SerializeField] private float health;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject shootPoint;
    [SerializeField] private GameObject[] superJumpPoints;

    [Header("Shooting variables")]
    [SerializeField] private float projectileStartingForce = 50;
    [SerializeField] private float projectileDamage = 4;
    [SerializeField] private float projectileDistanceForceMultiplier = 100;

    private void Awake()
    {
        EventSystem.Current.RegisterListener(typeof(EnemyHitEvent), TakeDamage);
        EventSystem.Current.RegisterListener(typeof(LilyPadDestroyedEvent), RemoveDestroyedLilypadFromList);
        statemachine = new StateMachine(this, states);
        startPosition = transform.position;
    }
    private void Update()
    {
        statemachine.Run();
    }
    private void RemoveDestroyedLilypadFromList(Callback.Event eb)
    {
        LilyPadDestroyedEvent e = (LilyPadDestroyedEvent)eb;
        lilypads.Remove(e.Pad.GetComponent<DestroyableLilypad>());
    }
    private void SinkAPad(Lilypads pad)
    {
        pad.setIsSInking(true);
    }
    public float GetProjectileStartingForce()
    {
        return projectileStartingForce;
    }
    public float GetProjectileDamage()
    {
        return projectileDamage;
    }
    public float GetProjectileDistanceForceMultiplier()
    {
        return projectileDistanceForceMultiplier;
    }
    public GameObject[] GetSuperJumpPoints()
    {
        return superJumpPoints;
    }
    public Vector3 GetStartPosition()
    {
        return startPosition;
    }
    public LayerMask GetCollisionMask()
    {
        return collisionMask;
    }
    public PlayerControl GetPlayer()
    {
        return player;
    }
    public GameObject GetProjectile()
    {
        return projectile;
    }
    public GameObject GetShootPoint()
    {
        return shootPoint;
    }
    public float GetHealth()
    {
        return health;
    }
    public void TakeDamage(Callback.Event eb)
    {
        EnemyHitEvent e = (EnemyHitEvent)eb;
        Debug.Log("BossDamageTaken");
        health -= e.Damage;
        if(health <= 0)
            statemachine.TransitionTo<BossDefeatedState>();
    }
}
