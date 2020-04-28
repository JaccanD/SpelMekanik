using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class PlayerSoundScript : MonoBehaviour
{
    [SerializeField] private AudioSource PlayerAudioSource;


    [Header("Sounds")]
    [SerializeField] private AudioClip PlayerHitSound;
    [SerializeField] private AudioClip PlayerPickupSound;

    private void Start()
    {
        EventSystem.Current.RegisterListener(typeof(PlayerHitEvent), OnPlayerHit);
        EventSystem.Current.RegisterListener(typeof(PickupEvent), OnPickup);
    }

    public void OnPlayerHit(Callback.Event eb)
    {
        PlayerAudioSource.PlayOneShot(PlayerHitSound);
    }
    public void OnPickup(Callback.Event eb)
    {
        PlayerAudioSource.PlayOneShot(PlayerPickupSound);
    }
}
