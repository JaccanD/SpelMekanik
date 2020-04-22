using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Callback
{
    public class HookHitEvent : Event
    {
        public GameObject Hook;
        public Vector3 Point;

        public HookHitEvent(GameObject hook, Vector3 point)
        {
            Hook = hook;
            Point = point;
        }
    }
}
