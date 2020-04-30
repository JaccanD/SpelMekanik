using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Callback
{
    public class QuestDoneEvent : Event
    {
        public GameObject NPC;

        public QuestDoneEvent(GameObject questGiver)
        {
            NPC = questGiver;
        }
    }
}
