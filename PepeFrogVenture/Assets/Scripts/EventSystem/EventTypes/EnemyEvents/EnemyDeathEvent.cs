using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Callback
{
    public class EnemyDeathEvent : Event
    {
        public Enemy enemy;

        public EnemyDeathEvent(Enemy deadEnemy)
        {
            enemy = deadEnemy;
        }
    }
}

