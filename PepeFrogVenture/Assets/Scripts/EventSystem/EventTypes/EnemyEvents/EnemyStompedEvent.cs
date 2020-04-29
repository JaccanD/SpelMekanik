using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Callback
{
    public class EnemyStompedEvent : Event
    {
        public GameObject enemyStomped;

        public EnemyStompedEvent(GameObject enemy)
        {
            enemyStomped = enemy;
        }
    }

}
