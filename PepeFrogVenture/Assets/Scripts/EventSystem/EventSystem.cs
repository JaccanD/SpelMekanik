using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Main Author: Jacob Didenbäck
// Secondary Author: Valter Falsterljung
namespace Callback
{
    public class EventSystem : MonoBehaviour
    {
        static private EventSystem __Current;
        public delegate void EventListener(Event ei);
        Dictionary<System.Type, List<EventListener>> eventListeners;

        List<System.Type> TypesWithListenersToRemove = new List<System.Type>();
        List<EventListener> ListenersToRemove = new List<EventListener>();
        static public EventSystem Current
        {
            get
            {
                if (__Current == null)
                {
                    __Current = GameObject.FindObjectOfType<EventSystem>();
                }

                return __Current;
            }
        }
        void OnEnable()
        {
            __Current = this;
        }
        public void RegisterListener(System.Type eventType, EventListener listener)
        {

            if (eventListeners == null)
            {
                eventListeners = new Dictionary<System.Type, List<EventListener>>();
            }
            if (eventListeners.ContainsKey(eventType) == false || eventListeners[eventType] == null)
            {
                eventListeners[eventType] = new List<EventListener>();
            }
            eventListeners[eventType].Add(listener);
        }
        public void UnRegisterListener(System.Type eventType, EventListener listener)
        {
            //Kan inte ta bort listeners direkt här ifall vi vill låta fiender ta bort sin listener när de dör
            //eftersom foreach loopen kommer sluta fungera när fienden tar bort sin listener och ändrar då listan
            //som foreach loopen itererar över.
            //
            if (eventListeners == null || !eventListeners.ContainsKey(eventType) || !eventListeners[eventType].Contains(listener))
            {
                return;
            }
            TypesWithListenersToRemove.Add(eventType);
            ListenersToRemove.Add(listener);
        }
        public void FireEvent(Event eventInfo)
        {
            System.Type trueEventInfoClass = eventInfo.GetType();
            if (!(eventListeners.ContainsKey(trueEventInfoClass)))
            {
                return;
            }
            if (eventListeners == null || eventListeners[trueEventInfoClass] == null)
            {
                return;
            }
            foreach (EventListener el in eventListeners[trueEventInfoClass])
            {
                el(eventInfo);
            }
        }
        //ändrade från fixedupdate till update
        private void Update()
        {
            RemoveListeners();
        }
        private void RemoveListeners()
        {
            foreach(System.Type T in TypesWithListenersToRemove)
            {
                foreach(EventListener L in ListenersToRemove)
                {
                    if (eventListeners[T].Contains(L))
                    {
                        eventListeners[T].Remove(L);
                    }
                }
            }
            TypesWithListenersToRemove.Clear();
            ListenersToRemove.Clear();
            
        }
    }
}