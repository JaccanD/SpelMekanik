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
    [SerializeField] private AudioClip MusicSound;

    

    private void Start()
    {
        EventSystem.Current.RegisterListener(typeof(PlayerHitEvent), OnPlayerHit);
        EventSystem.Current.RegisterListener(typeof(PickupEvent), OnPickup);
        // Eventgrej maybe? hoppljud iaf

        playMusic();
    }

    private void Update()
    {
        if (!MusicAudioSource.isPlaying)
        {
            playMusic();
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

    private void playMusic() // Jack
    {
        MusicAudioSource.PlayOneShot(MusicSound);
        MusicAudioSource.volume = 0.3f;
    }
}
