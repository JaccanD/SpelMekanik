using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Callback
{
    public class PlayerSeenEvent : Event
    {
        public Vector3 EnemyPosition;

        public PlayerSeenEvent(Vector3 position)
        {
            EnemyPosition = position;
        }
    }
}
