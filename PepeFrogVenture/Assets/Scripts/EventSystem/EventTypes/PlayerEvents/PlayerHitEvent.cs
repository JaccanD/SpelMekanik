using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Callback
{
    public class PlayerHitEvent : Event
    {
        public GameObject PlayerGameObject;
        public float Damage;
        public bool enemyHit = true;

        public PlayerHitEvent(GameObject player, float damage)
        {
            PlayerGameObject = player;
            Damage = damage;
        }

        public PlayerHitEvent(GameObject player, float damage, bool source)
        {
            PlayerGameObject = player;
            Damage = damage;
            enemyHit = source;
        }

    }
}
