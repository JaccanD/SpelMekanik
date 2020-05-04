using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Callback;
// Author: August Brunnsätter
public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        EventSystem.Current.RegisterListener(typeof(EnemyHitEvent), BossLoseHealth);
    }

    public void BossLoseHealth(Callback.Event eb)
    {
        EnemyHitEvent e = (EnemyHitEvent)eb;

        slider.value -= e.Damage;
    }
}

