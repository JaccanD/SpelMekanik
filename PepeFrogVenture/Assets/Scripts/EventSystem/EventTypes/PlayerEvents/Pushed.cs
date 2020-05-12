using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Callback
{
    public class Pushed : Event
    {
        public GameObject Player;
        public float PushBackStrenght;
        public float Height;
        public Vector3 Origin;
        public float StunDuration;

        public Pushed(GameObject playerObject, Vector3 hittingEnemyPosition, float pushBackValue, float height)
        {
            Player = playerObject;
            PushBackStrenght = pushBackValue;
            Origin = hittingEnemyPosition;
            Height = height;
            StunDuration = 0;
        }

        public Pushed(GameObject playerObject, Vector3 hittingEnemyPosition, float pushBackValue, float height, float stunDuration)
        {
            Player = playerObject;
            PushBackStrenght = pushBackValue;
            Origin = hittingEnemyPosition;
            Height = height;
            StunDuration = stunDuration;
        }
    }
}

