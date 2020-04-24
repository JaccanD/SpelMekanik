using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Callback;

public class BlueBerryCounter : MonoBehaviour
{
    public Text BlueBerryCount;
    private int BlueBerryInt = 0;

    private void Start()
    {
        EventSystem.Current.RegisterListener(typeof(PickupEvent), GainBlueBerry);
    }

    public void GainBlueBerry(Callback.Event eb)
    {
        PickupEvent e = (PickupEvent)eb;

        if (e.Pickup.tag == "Berry")
        {
            BlueBerryInt++;
            BlueBerryCount.text = BlueBerryInt.ToString();
        }
    }
}
