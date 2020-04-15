using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Callback
{
    public class EventSystem : MonoBehaviour
    {

        // Start is called before the first frame update
        void Start()
        {

        }
        static private EventSystem __Current;
        static public EventSystem Current
        {
            get
            {
                if(__Current == null)
                {
                    __Current = GameObject.FindObjectOfType<EventSystem>();
                }

                return __Current;
            }
        }
        public delegate void EventListener(Event ei);

        Dictionary<System.Type, List<EventListener>> eventListeners;

        public void RegisterListener<T>(System.Action<T> listener) where T : Event
        {
            System.Type eventType = typeof(T);
            if (eventListeners == null)
            {
                eventListeners = new Dictionary<System.Type, List<EventListener>>();
            }

            if(eventListeners.ContainsKey(eventType) == false || eventListeners[eventType] == null)
            {
                eventListeners[eventType] = new List<EventListener>();
            }
            EventListener wrapper = (ei) => { listener((T)ei); };
            eventListeners[eventType].Add(wrapper);

        }

        public void UnRegisterListener<T>(System.Action<T> listener) where T : Event
        {
            System.Type eventType = typeof(T);
            EventListener wrapper = (ei) => { listener((T)ei); };
            if (!eventListeners[eventType].Contains(wrapper))
            {
                return;
            }
            eventListeners[eventType].Remove(wrapper);
        }

        public void FireEvent(Event eventInfo)
        {
            System.Type trueEventInfoClass = eventInfo.GetType();
            if(eventListeners == null || eventListeners[trueEventInfoClass] == null)
            {
                return;
            }

            foreach(EventListener el in eventListeners[trueEventInfoClass])
            {
                el(eventInfo);
            }
        }



    }
}
