using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Callback
{
    public class LilyPadDestroyedEvent : Event
    {
        public GameObject Pad;
        public LilyPadDestroyedEvent(GameObject newPad)
        {
            Pad = newPad;
        }
    }
}