using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Callback
{
    public class NPCDialogueEvent : Event
    {
        public string Text;
        
        public NPCDialogueEvent(string newText)
        {
            Text = newText;
        }
    }
}