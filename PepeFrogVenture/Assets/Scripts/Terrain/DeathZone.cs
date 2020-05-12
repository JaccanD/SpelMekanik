using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

// Author: Jacob Didenbäck
public class DeathZone : MonoBehaviour
{
    BoxCollider coll;
    [SerializeField]private ParticleSystem waterSplash;
    private float timer = 0;
    [SerializeField] private AudioClip WaterSound;
    [SerializeField] private float damage = 5;
    [SerializeField] private float pushbackForce = 5;
    [SerializeField] private float pushbackHeight = 5;
    private AudioSource source;

    public void Awake()
    {
        coll = GetComponent<BoxCollider>();
        source = GetComponent<AudioSource>();
        
    }
    public void Update()
    {

        Collider[] overLaps = Physics.OverlapBox(transform.position + coll.center, coll.bounds.extents, transform.rotation);
        for (int i = 0; i < overLaps.Length; i++)
        {
            if (overLaps[i].transform.gameObject.tag == "Player" && timer <= 0)
            {
                GameObject player = overLaps[i].transform.gameObject;
                timer = 2;
                ParticleSystem splash = GameObject.Instantiate(waterSplash, overLaps[i].transform);
                splash.transform.parent = null;
                EventSystem.Current.FireEvent(new PlayerHitEvent(overLaps[i].transform.gameObject, damage));
                EventSystem.Current.FireEvent(new Pushed(player, player.transform.position, pushbackForce, pushbackHeight));
                source.volume = 0.5f;
                source.PlayOneShot(WaterSound);
                break;
            }
            if (timer < 0) timer = 0;
        }
        timer -= Time.deltaTime;
    }
}
