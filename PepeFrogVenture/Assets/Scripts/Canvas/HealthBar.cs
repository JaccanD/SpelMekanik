﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Callback;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        EventSystem.Current.RegisterListener(typeof(PlayerHitEvent), LoseHealth);
        EventSystem.Current.RegisterListener(typeof(PickupEvent), GainHealth);
    }

    public void LoseHealth(Callback.Event eb)
    {
        PlayerHitEvent e = (PlayerHitEvent)eb;

        slider.value -= e.Damage;
    }
    public void GainHealth(Callback.Event eb)
    {
        PickupEvent e = (PickupEvent)eb;

        if(e.Pickup.tag == "Flies")
        {
            slider.value += 2;
        }
    }



}
