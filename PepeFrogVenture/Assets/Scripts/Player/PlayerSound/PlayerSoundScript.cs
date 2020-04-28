using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class PlayerSoundScript : MonoBehaviour
{
    [SerializeField] private AudioSource PlayerAudioSource;


    [Header("Sounds")]
    [SerializeField] private AudioClip PlayerHitSound;

    private void Start()
    {
        EventSystem.Current.RegisterListener(typeof(PlayerHitEvent), OnPlayerHit);
    }

    public void OnPlayerHit(Callback.Event eb)
    {
        PlayerAudioSource.PlayOneShot(PlayerHitSound);
    }
}
