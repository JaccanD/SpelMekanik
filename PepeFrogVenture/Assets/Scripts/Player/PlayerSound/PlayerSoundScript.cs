using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class PlayerSoundScript : MonoBehaviour
{
    [SerializeField] private AudioSource PlayerAudioSource;
    [SerializeField] private AudioSource MusicAudioSource; //Jack 



    [Header("Sounds")]
    [SerializeField] private AudioClip PlayerHitSound;
    [SerializeField] private AudioClip PlayerPickupSound;
    [SerializeField] private AudioClip PlayerJumpSound;
    [SerializeField] private AudioClip MusicSound; // Jack
    [SerializeField] private AudioClip ToungeOut; // Jack




    private void Start()
    {
        EventSystem.Current.RegisterListener(typeof(PlayerHitEvent), OnPlayerHit);
        EventSystem.Current.RegisterListener(typeof(PickupEvent), OnPickup);
        EventSystem.Current.RegisterListener(typeof(PlayerJumpEvent), OnPlayerJump); // Jack
        EventSystem.Current.RegisterListener(typeof(ToungeFlickEvent), OnToungeOut); // Jack

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
        PlayerAudioSource.PlayOneShot(PlayerHitSound);
    }
    public void OnPickup(Callback.Event eb)
    {
        PlayerAudioSource.PlayOneShot(PlayerPickupSound);
    }

    public void PlayMusic() // Jack
    {
        MusicAudioSource.PlayOneShot(MusicSound);
        MusicAudioSource.volume = 0.3f;
    }

    public void OnPlayerJump(Callback.Event eb) // Jack
    {
        PlayerAudioSource.pitch = Random.Range(1.2f, 1.6f);
        PlayerAudioSource.PlayOneShot(PlayerJumpSound);
    }

    public void OnToungeOut(Callback.Event eb) // Jack
    {
        PlayerAudioSource.PlayOneShot(ToungeOut);
    }  
}
