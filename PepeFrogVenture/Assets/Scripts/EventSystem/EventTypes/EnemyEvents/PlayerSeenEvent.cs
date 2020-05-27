using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Callback
{
    public class PlayerSeenEvent : Event
    {
        public Vector3 EnemyPosition;
        public GameObject enemy;

        public PlayerSeenEvent(Vector3 position, GameObject enemyObject)
        {
            EnemyPosition = position;
            enemy = enemyObject;
        }
    }
}
