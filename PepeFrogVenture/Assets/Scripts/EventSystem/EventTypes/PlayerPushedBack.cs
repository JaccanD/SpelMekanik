using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Callback
{
    public class EnemyPushesPlayerBack : Event
    {
        public GameObject player;
        public float pushBackStrenght;
        public Vector3 enemyPosition;

        public EnemyPushesPlayerBack(GameObject playerObject, Vector3 hittingEnemyPosition, float pushBackValue)
        {
            player = playerObject;
            pushBackStrenght = pushBackValue;
            enemyPosition = hittingEnemyPosition;
        }
    }
}

