using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Callback
{
    public class RespawnPointReachedEvent : Event
    {
        public GameObject RespawnPoint;

        public RespawnPointReachedEvent(GameObject respawnPoint)
        {
            RespawnPoint = respawnPoint;
        }
    }
}
