using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;
using System;

// Main Author: Jack Noaksson
// Secondary Author: Jacob Didenbäck
public class PlayerSoundScript : MonoBehaviour
{
    private float health;

    [SerializeField] private AudioSource PlayerAudioSource;
    [SerializeField] private AudioSource MusicAudioSource; //Jack 
    [SerializeField] private AudioSource LowHealthAudioSource; //Jack 



    [Header("Sounds")]
    [SerializeField] private AudioClip PlayerHitSound;
    [SerializeField] private AudioClip PlayerPickupSound;
    [SerializeField] private AudioClip PlayerJumpSound;
    [SerializeField] private AudioClip MusicSound; // Jack
    [SerializeField] private AudioClip ToungeOut; // Jack
    [SerializeField] private AudioClip Fireball; // Jack
    [SerializeField] private AudioClip LowHealth; // Jack
    [SerializeField] private AudioClip PlayerDeath; // Jack



    private void Start()
    {
        EventSystem.Current.RegisterListener(typeof(PlayerHitEvent), OnPlayerHit);
        EventSystem.Current.RegisterListener(typeof(PickupEvent), OnPickup);
        EventSystem.Current.RegisterListener(typeof(PlayerJumpEvent), OnPlayerJump); // Jack
        EventSystem.Current.RegisterListener(typeof(ToungeFlickEvent), OnToungeOut); // Jack
        EventSystem.Current.RegisterListener(typeof(FireballshotEvent), OnFireballShot); // Jack
        EventSystem.Current.RegisterListener(typeof(PlayerDeathEvent), OnDeath); // Jack

        PlayMusic(); // Jack

    }

    private void Update() //Jack
    {
        if (!MusicAudioSource.isPlaying)
        {
            PlayMusic();

        }
    }

    public void OnPlayerHit(Callback.Event eb)
    {
        PlayerHitEvent e = (PlayerHitEvent)eb;
        PlayerAudioSource.PlayOneShot(PlayerHitSound); 
        PlayerAudioSource.pitch = UnityEngine.Random.Range(1f, 1.3f);


        health = PlayerStats.getHealth();
        if (health - e.Damage  <= e.Damage)
        {
            OnLowHealth();
        } 
    }
    public void OnPickup(Callback.Event eb)
    {
        PickupEvent e = (PickupEvent)eb;
        if(e.Pickup.tag != "Flies")
        {
            return;
        }
        PlayerAudioSource.PlayOneShot(PlayerPickupSound);
        PlayerAudioSource.pitch = UnityEngine.Random.Range(1f, 1.3f);
        if(health > 2)
        {
            LowHealthAudioSource.Stop();
        }
    }

    public void PlayMusic() // Jack
    {
        MusicAudioSource.PlayOneShot(MusicSound);
        MusicAudioSource.volume = 0.3f;
    }

    public void OnPlayerJump(Callback.Event eb) // Jack
    {
        PlayerAudioSource.pitch = UnityEngine.Random.Range(1f, 1.3f);
        PlayerAudioSource.PlayOneShot(PlayerJumpSound);
    }

    public void OnToungeOut(Callback.Event eb) // Jack
    {
        PlayerAudioSource.PlayOneShot(ToungeOut);
    }  

    public void OnFireballShot(Callback.Event eb){
        PlayerAudioSource.PlayOneShot(Fireball);    
    }

    public void OnLowHealth()
    {
        LowHealthAudioSource.volume = 0.3f;
        LowHealthAudioSource.Play();
        
    }
    
    public void OnDeath(Callback.Event eb){
        PlayerAudioSource.PlayOneShot(PlayerDeath); // Jack
        LowHealthAudioSource.Stop();

    }
}
