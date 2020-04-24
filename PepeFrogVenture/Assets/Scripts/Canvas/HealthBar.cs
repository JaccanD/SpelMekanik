using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

//<<<<<<< HEAD:PepeFrogVenture/Assets/Scripts/Canvas/HealthBar.cs
//=======
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
//>>>>>>> 6aec782d7a27020c948adc3af0c64e867cd09aac:PepeFrogVenture/Assets/Scripts/Player/HealthBar.cs
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
