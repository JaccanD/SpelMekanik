using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Callback
{
    public class FireFlyDeathEvent : Event
    {
        public GameObject Parent;

        public FireFlyDeathEvent(GameObject newParent)
        {
            Parent = newParent;
        }
    }
}