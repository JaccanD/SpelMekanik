using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Callback
{
    public class DebugListener : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            EventSystem.Current.RegisterListener<DebugEvent>(OnDebugEvent);
        }

        // Update is called once per frame
        void Update()
        {

        }
        void OnDebugEvent(DebugEvent e)
        {
            Debug.Log(e.DebugText);
        }
    }
}
