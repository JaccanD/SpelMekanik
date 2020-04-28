using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Callback;

public class FireReady : MonoBehaviour
{
    public Image fireImage;

    private void Start()
    {
        fireImage.enabled = false;
        EventSystem.Current.RegisterListener(typeof(PickupEvent), FireBelly);
        EventSystem.Current.RegisterListener(typeof(FireballshotEvent), LoseFireBelly);
        EventSystem.Current.RegisterListener(typeof(PlayerDeathEvent), LoseFireBellyonDeath);
    }

    public void FireBelly(Callback.Event eb)
    {
        PickupEvent e = (PickupEvent)eb;

        if (e.Pickup.tag == "Fire")
        {
            fireImage.enabled = true;
        }
    }

    public void LoseFireBelly(Callback.Event eb)
    {
        fireImage.enabled = false;
    }

    public void LoseFireBellyonDeath(Callback.Event eb)
    {
        fireImage.enabled = false;
    }
}
