using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Callback;
// Main Author: Valter Falsterljung
// Secondary Author: Jacob Didenbäck
public class Enemy : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent agent;
    protected StateMachine statemachine;
    public State[] states;
    public PlayerControl player;
    public float speed = 2;
    private new BoxCollider collider;
    [SerializeField] LayerMask CollisionMask;
    [SerializeField] private GameObject[] PatrolPoints;
    [SerializeField] private GameController controller;
    [SerializeField] protected float damage = 2;
    [SerializeField] protected float Health = 4;
    [SerializeField] private Vector3 stompedScale;
    [SerializeField] private float spotPlayerDistance;
    [SerializeField] private float lostPlayerDistance;

    private void Awake()
    {
        GameObject PlayerGo = GameObject.FindGameObjectWithTag("Player");
        player = PlayerGo.GetComponent<PlayerControl>();
        EventSystem.Current.RegisterListener(typeof(EnemyHitEvent), OnHit);
        EventSystem.Current.RegisterListener(typeof(EnemyStompedEvent), Stomped);
        EventSystem.Current.RegisterListener(typeof(PlayerLostEvent), PlayerLost);
        collider = GetComponent<BoxCollider>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        statemachine = new StateMachine(this, states);
    }

    private void Update()
    {
        statemachine.Run();
    }
    public float GetSpotPlayerDistance()
    {
        return spotPlayerDistance;
    }
    public float GetLostPlayerDistance()
    {
        return lostPlayerDistance;
    }
    public GameObject[] getPatrolPoints()
    {
        return PatrolPoints;
    }
    public void setPatrolPoints(GameObject[] newPoints)
    {
        PatrolPoints = newPoints;
    }
    public float getDamage()
    {
        return damage;
    }
    public float GetSpeed()
    {
        return speed;
    }
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    public LayerMask GetCollisionMask()
    {
        return CollisionMask;

    }
    public BoxCollider getCollider()
    {
        return collider;
    }
    public Transform GetTransform()
    {
        return transform;
    }
    private void PlayerLost(Callback.Event eb)
    {
        statemachine.TransitionTo<EnemyWalkingState>();
    }
    public void Defeated()
    {
        EventSystem.Current.UnRegisterListener(typeof(EnemyHitEvent), OnHit);
        EventSystem.Current.UnRegisterListener(typeof(EnemyStompedEvent), Stomped);
        EventSystem.Current.UnRegisterListener(typeof(PlayerLostEvent), PlayerLost);

        statemachine.TransitionTo<EnemyDefeatedState>();
    }
    public GameController getGamecontroller()
    {
        return controller;
    }
    public void Stomped(Callback.Event eb)
    {
        EnemyStompedEvent e = (EnemyStompedEvent)eb;
        if (e.enemyStomped != gameObject)
            return;
        transform.localScale = stompedScale;
        Defeated();
    }
    public void OnHit(Callback.Event eb)
    {
        EnemyHitEvent e = (EnemyHitEvent)eb;
        if (e.EnemyHit != gameObject)
            return;
        Health -= e.Damage;
        if (Health <= 0)
        {

            Defeated();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EventSystem.Current.FireEvent(new EnemyStompedEvent(gameObject));
        }

    }
}
