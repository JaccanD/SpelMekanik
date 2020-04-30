using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class PlayerSoundScript : MonoBehaviour
{
    [SerializeField] private AudioSource PlayerAudioSource;
    [SerializeField] private AudioSource MusicAudioSource; //Jack 
    [SerializeField] private AudioSource FootstepsSource; //Jack 


    [Header("Sounds")]
    [SerializeField] private AudioClip PlayerHitSound;
    [SerializeField] private AudioClip PlayerPickupSound;
    [SerializeField] private AudioClip PlayerJumpSound;
    [SerializeField] private AudioClip MusicSound; // Jack
    [SerializeField] private AudioClip ToungeOut; // Jack
 //   [SerializeField] private AudioClip Footstep; // Jack
 
    //------------------------------------------------------------------------------
    private double time; // Jack
    private float filterTime; // Jack
    private string colliderType; // Jack
    public AudioClip defaultSound; // Jack                      //Detta är bara betan tills animationerna är klara
    public AudioClip stockSound; // Jack
    public AudioClip lilySound; // Jack
    //------------------------------------------------------------------------------




    private void Start()
    {
        EventSystem.Current.RegisterListener(typeof(PlayerHitEvent), OnPlayerHit);
        EventSystem.Current.RegisterListener(typeof(PickupEvent), OnPickup);
        EventSystem.Current.RegisterListener(typeof(PlayerJumpEvent), OnPlayerJump); // Jack
        EventSystem.Current.RegisterListener(typeof(ToungeFlickEvent), OnToungeOut); // Jack

        PlayMusic(); // Jack
       //------------------------------------------------------------------------------
        time = AudioSettings.dspTime; // Jack
        filterTime = 0.2f;                                      // Jack//Detta är bara betan tills animationerna är klara
       //------------------------------------------------------------------------------

    }

    private void Update() //Jack
    {
        if (!MusicAudioSource.isPlaying)
        {
            PlayMusic();
        }


        //------------------------------------------------------------------------------
        void OnCollisionEnter(Collision col) // Jack
        {
            SurfaceColliderType act = col.gameObject.GetComponent<Collider>().gameObject.GetComponent<SurfaceColliderType>();

            if (act)
            {
                colliderType = act.GetTerrainType();                            //Detta är bara betan tills animationerna är klara
            }

        }
        //------------------------------------------------------------------------------




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
    //------------------------------------------------------------------------------
    private void PlayDynamicFootstepSound(int footnumber) // Jack
    {
        if (AudioSettings.dspTime < time + filterTime) 
        {
            return;
        }

        switch (colliderType)
        {
            case "Stock":                                           //Detta är bara betan tills animationerna är klara
                FootstepsSource.PlayOneShot(stockSound);
                break;
            case "Lily":
                FootstepsSource.PlayOneShot(lilySound);
                break;
            default:
                FootstepsSource.PlayOneShot(defaultSound);
                break;
        }

        time = AudioSettings.dspTime;
    }
    //------------------------------------------------------------------------------

}
