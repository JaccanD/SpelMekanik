using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;
// Author: Jacob Didenbäck
public class FireFlyOnDestroy : MonoBehaviour
{
    public GameObject Parent;
    //private void OnDestroy()
    //{
    //    if(Parent != null)
    //        EventSystem.Current.FireEvent(new FireFlyDeathEvent(Parent));
    //}
    //ändrat för att passa med resten av setactive ändringarna
    private void OnDisable()
    {
        if (Parent != null)
            EventSystem.Current.FireEvent(new FireFlyDeathEvent(Parent));
    }
}
