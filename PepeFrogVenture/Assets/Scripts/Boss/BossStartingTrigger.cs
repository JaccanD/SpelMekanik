using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

// Author: Valter Fallsterljung

public class BossStartingTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            EventSystem.Current.FireEvent(new BossBattleStartingEvent());
            gameObject.SetActive(false);
        }
    }
}
