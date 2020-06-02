using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Callback;
// Author: August Brunnsätter
public class BlueBerryCounter : MonoBehaviour
{
    [SerializeField] private Slider berrySlider;

    [SerializeField] private int[] neededBerries;
    private int questIndex = 0;
    private int BlueBerryInt = 0;

    private void Start()
    {
        berrySlider.value = 1;
        EventSystem.Current.RegisterListener(typeof(PickupEvent), GainBlueBerry);
        EventSystem.Current.RegisterListener(typeof(QuestDoneEvent), ResetBlueBerry);
    }

    public void GainBlueBerry(Callback.Event eb)
    {
        PickupEvent e = (PickupEvent)eb;

        if (e.Pickup.tag == "Berry")
        {
            BlueBerryInt++;

            berrySlider.value = BlueBerryInt;
        }
    }

    public void ResetBlueBerry(Callback.Event eb)
    {
        QuestDoneEvent e = (QuestDoneEvent)eb;

        BlueBerryInt = 0;

        questIndex++;
        questIndex = questIndex % neededBerries.Length;

        if (neededBerries[questIndex] == 0)
        {
            berrySlider.maxValue = 1;
            berrySlider.value = 1;
        }
        else
        {
            berrySlider.maxValue = neededBerries[questIndex];
            berrySlider.value = BlueBerryInt;
        }
    }
}
