using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;


namespace Callback
{
    public class PlayerRespawnEvent : Event
    {
        public GameObject RespawnPoint;

        public PlayerRespawnEvent(GameObject respawn)
        {
            RespawnPoint = respawn;
        }
    }
}

