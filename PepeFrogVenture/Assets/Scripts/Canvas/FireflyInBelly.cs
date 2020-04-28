using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Callback;

public class FireflyInBelly : MonoBehaviour
{
    public ParticleSystem BellyGlow;

    private void Start()
    {
        BellyGlow.Stop();
        EventSystem.Current.RegisterListener(typeof(PickupEvent), FireBelly);
        EventSystem.Current.RegisterListener(typeof(FireballshotEvent), LoseFireBelly);
        EventSystem.Current.RegisterListener(typeof(PlayerDeathEvent), LoseFireBellyonDeath);
    }

    public void FireBelly(Callback.Event eb)
    {
        PickupEvent e = (PickupEvent)eb;

        if (e.Pickup.tag == "Fire")
        {
            BellyGlow.Play();
        }
    }

    public void LoseFireBelly(Callback.Event eb)
    {
        BellyGlow.Stop();
    }

    public void LoseFireBellyonDeath(Callback.Event eb)
    {
        BellyGlow.Stop();
    }
    //test
}
