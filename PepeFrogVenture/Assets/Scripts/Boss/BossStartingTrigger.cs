using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class BossStartingTrigger : MonoBehaviour
{
    new private BoxCollider collider;


    // Bara för memes skull. Ta bort efter Speltest. 

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
