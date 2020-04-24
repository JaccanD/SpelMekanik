﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class BossStartingTrigger : MonoBehaviour
{
    new private BoxCollider collider;

    private void Awake()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            EventSystem.Current.FireEvent(new BossBattleStartingEvent());
        }

    }
}