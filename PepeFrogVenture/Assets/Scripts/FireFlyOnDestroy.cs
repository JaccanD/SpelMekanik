using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class FireFlyOnDestroy : MonoBehaviour
{
    public GameObject Parent;
    private void OnDestroy()
    {
        EventSystem.Current.FireEvent(new FireFlyDeathEvent(Parent));
    }
}
