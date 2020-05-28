using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

// Main Author: August Brunsätter
public class BossMotionControl: MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        EventSystem.Current.RegisterListener(typeof(BossDivingEvent), BossDive);
        EventSystem.Current.RegisterListener(typeof(BossShootingEvent), BossAttack);
        EventSystem.Current.RegisterListener(typeof(BossChargeAttackEvent), BossChargeAttack);
        EventSystem.Current.RegisterListener(typeof(BossRapidAttackEvent), BossRapidAttack);
        EventSystem.Current.RegisterListener(typeof(BossSuperAttackEvent), BossSuperAttack);
        EventSystem.Current.RegisterListener(typeof(BossDeadEvent), BossDead);
    }

    void Update()
    {

    }

    public void BossDive(Callback.Event eb)
    {
        anim.SetTrigger("Dive");
    }

    public void BossAttack(Callback.Event eb)
    {
        anim.SetTrigger("Attack");
    }

    public void BossChargeAttack(Callback.Event eb)
    {
        anim.SetTrigger("ChargeAttack");
    }

    public void BossRapidAttack(Callback.Event eb)
    {
        anim.SetTrigger("RapidAttack");
    }

    public void BossSuperAttack(Callback.Event eb)
    {
        anim.SetTrigger("SuperAttack");
    }

    public void BossDead(Callback.Event eb)
    {
        anim.SetTrigger("BossDead");
    }
}
