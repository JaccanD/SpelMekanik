using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Callback
{
    public class PickupEvent : Event
    {
        public GameObject Pickup;

        public PickupEvent(GameObject pu)
        {
            Pickup = pu;
        }
    }
}