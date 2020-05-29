using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Callback
{ 
    public class PlayerWalkingEvent : Event
    {
        public string groundTag;

        public PlayerWalkingEvent(string tag)
        {
            groundTag = tag;
        }
    }
}
