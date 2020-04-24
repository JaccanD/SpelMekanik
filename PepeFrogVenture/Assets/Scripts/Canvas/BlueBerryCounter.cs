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
        EventSystem.Current.RegisterListener(typeof(QuestDoneEvent), ResetBlueBerry);
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

    public void ResetBlueBerry(Callback.Event eb)
    {
        QuestDoneEvent e = (QuestDoneEvent)eb;

        BlueBerryInt = 0;
        BlueBerryCount.text = BlueBerryInt.ToString();
    }
}
