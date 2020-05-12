using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

// Author: Jacob Didenbäck
public class DeathZone : MonoBehaviour
{
    BoxCollider coll;
    new private BoxCollider collider;
    [SerializeField]private ParticleSystem waterSplash;
    private float timer = 0;
    [SerializeField] private AudioClip WaterSound;
    [SerializeField] private float damage = 5;
    [SerializeField] private float pushbackForce = 5;
    [SerializeField] private float pushbackHeight = 5;
    private AudioSource source;

    public void Awake()
    {
        collider = GetComponent<BoxCollider>();
        source = GetComponent<AudioSource>();
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Player")
        {
            return;
        }
        GameObject player = other.gameObject;
        ParticleSystem splash = GameObject.Instantiate(waterSplash, player.transform);
        splash.transform.parent = null;
        EventSystem.Current.FireEvent(new PlayerHitEvent(player, damage));
        EventSystem.Current.FireEvent(new Pushed(player, player.transform.position, pushbackForce, pushbackHeight));
        source.volume = 0.5f;
        source.PlayOneShot(WaterSound);
    }
}
