using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Callback
{
    public class EnemyAttackingEvent : Event
    {
        public GameObject enemy;
        public EnemyAttackingEvent(GameObject enemyObject)
        {
            enemy = enemyObject;
        }
    }
}

