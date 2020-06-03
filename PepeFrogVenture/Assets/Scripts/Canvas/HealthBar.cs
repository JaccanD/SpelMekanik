using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Callback;

// Main Author: August Brunnsätter
// Secondary Author: Jacob Didenbäck
public class HealthBar : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        GameObject Controller = GameObject.FindGameObjectWithTag("GameController");
        //slider.value = Controller.GetComponent<GameController>().Health;
        slider.value = PlayerStats.getHealth();
        EventSystem.Current.RegisterListener(typeof(PlayerHitEvent), LoseHealth);
        EventSystem.Current.RegisterListener(typeof(PickupEvent), GainHealth);
        EventSystem.Current.RegisterListener(typeof(PlayerRespawnEvent), ResetBar);
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
    public void ResetBar(Callback.Event eb)
    {
        slider.value = slider.maxValue;
    }



}
