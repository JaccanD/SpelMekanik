using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Callback
{
    public class EnemyHitEvent : Event
    {
        public GameObject EnemyHit;
        public float Damage;

        public EnemyHitEvent(GameObject enemy, float damage)
        {
            EnemyHit = enemy;
            Damage = damage;
        }
    }
}
