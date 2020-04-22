using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Callback
{
    public class NPCDialogueEvent : Event
    {
        public string Line;

        public NPCDialogueEvent(string newLine)
        {
            Line = newLine;
        }
    }
}
