using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

// Author: Valter Falsterljung
public class BossSoundScript : MonoBehaviour
{

    [SerializeField] private AudioSource bossSound;


    [Header("Sounds")]
    [SerializeField] private AudioClip bossSpit;




    // Start is called before the first frame update
    void Start()
    {
        EventSystem.Current.RegisterListener(typeof(BossShootingEvent), OnBossSpit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBossSpit(Callback.Event eb)
    {
        bossSound.volume = 0.5f;
        bossSound.PlayOneShot(bossSpit);
    }
}
