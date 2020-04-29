using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            //eventListeners[eventType].Remove(listener);
        }
        public void FireEvent(Event eventInfo)
        {
            System.Type trueEventInfoClass = eventInfo.GetType();
            if (!eventListeners.ContainsKey(trueEventInfoClass))
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
        private void FixedUpdate()
        {
            // Kör fixed update för att jag kan inte se någon anledning till att vi behöver
            // ta bort listeners oftare än fixed update kör.

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
                        Debug.Log("Listener Removed");
                    }
                }
            }
            TypesWithListenersToRemove.Clear();
            ListenersToRemove.Clear();
            
        }
    }
}