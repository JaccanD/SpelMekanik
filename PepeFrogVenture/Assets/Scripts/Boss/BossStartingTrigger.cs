using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

// Author: Valter Falsterljung

public class BossStartingTrigger : MonoBehaviour
{
    new private BoxCollider collider;
    private AudioSource sound;

    private void Awake()
    {
        sound = GetComponentInChildren<AudioSource>();
        collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            EventSystem.Current.FireEvent(new BossBattleStartingEvent());
            sound.Play();
        }
    }
}
