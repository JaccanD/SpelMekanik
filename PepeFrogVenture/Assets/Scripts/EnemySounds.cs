using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class EnemySounds : MonoBehaviour
{

    [SerializeField] private AudioSource source;

    [SerializeField] private AudioClip EnemyDead;


    // Start is called before the first frame update
    void Start()
    {
        EventSystem.Current.RegisterListener(typeof(EnemyHitEvent), OnEnemyHit);
    }

    public void OnEnemyHit(Callback.Event eb) // Jack
    {
        source.volume = 0.2f;
        source.PlayOneShot(EnemyDead);
    }
}