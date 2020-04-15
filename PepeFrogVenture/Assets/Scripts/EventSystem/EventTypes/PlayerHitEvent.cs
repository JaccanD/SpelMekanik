using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Callback
{
    public class PlayerHitEvent : Event
    {
        public GameObject PlayerGameObject;
        public float Damage;

        public PlayerHitEvent(GameObject player, float damage)
        {
            PlayerGameObject = player;
            Damage = damage;
        }
    }
}
