using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

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
        bossSound.PlayOneShot(bossSpit);
    }
}
