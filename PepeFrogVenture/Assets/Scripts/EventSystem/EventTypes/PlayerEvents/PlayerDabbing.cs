using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Callback
{
    public class PlayerDabbing : Event
    {
        public Vector3 dabLocation;

        public PlayerDabbing(Vector3 location)
        {
            dabLocation = location;
        }
    }

}
