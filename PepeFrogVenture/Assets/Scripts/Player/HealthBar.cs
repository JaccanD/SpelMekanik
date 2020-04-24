using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

<<<<<<< HEAD
=======
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
>>>>>>> parent of 0532aa8... tdh
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
