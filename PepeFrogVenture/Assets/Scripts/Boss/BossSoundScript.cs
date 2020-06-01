using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

// Author: Valter Fallsterljung? 
// Jack (Designer) har också varit här
public class BossSoundScript : MonoBehaviour
{

    [SerializeField] private AudioSource bossSound;


    [Header("Sounds")]
    [SerializeField] private AudioClip bossSpit;
    [SerializeField] private AudioClip bossRapidAttack;
    [SerializeField] private AudioClip bossSuperAttack;
    [SerializeField] private AudioClip bossChargeAttack;



    void Start()
    {
        EventSystem.Current.RegisterListener(typeof(BossShootingEvent), OnBossSpit);
        EventSystem.Current.RegisterListener(typeof(BossRapidAttackEvent), OnBossRapidAttack);
        EventSystem.Current.RegisterListener(typeof(BossSuperAttackEvent), OnBossSuperAttack);
        EventSystem.Current.RegisterListener(typeof(BossChargeAttackEvent), OnBossChargeAttack);


    }
    public void OnBossSpit(Callback.Event eb)
    {
        bossSound.volume = 0.5f;
        bossSound.PlayOneShot(bossSpit);
    }

    public void OnBossRapidAttack(Callback.Event eb)
    {
        bossSound.volume = 0.5f;
        bossSound.PlayOneShot(bossRapidAttack);
    }

    public void OnBossSuperAttack(Callback.Event eb)
    {
        bossSound.volume = 0.5f;
        bossSound.PlayOneShot(bossSuperAttack);
    }

    public void OnBossChargeAttack(Callback.Event eb)
    {
        bossSound.volume = 0.5f;
        bossSound.PlayOneShot(bossChargeAttack);
    }
}
